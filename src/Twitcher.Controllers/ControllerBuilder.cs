using System;
using System.Collections.Generic;
using System.Reflection;
using TwitchLib.Client.Events;
using TwitchLib.Client.Interfaces;
using System.Linq;
using Twitcher.Controllers.Attributes;
using TwitchLib.Client.Models;
using System.Threading;
using Twitcher.Controllers.Services;

namespace Twitcher.Controllers
{
    public class ControllerBuilder
    {
        private TwitcherClient _client;
        private ControllerManager _manager;

        public ControllerBuilder(TwitcherClient client)
        {
            _client = client;
            _manager = new ControllerManager();
        }
        public TwitcherClient BuildControllers()
        {
            RegisterControllers();
            _client.Bot.OnJoinedChannel += (object sender, OnJoinedChannelArgs args) => {
                Dictionary<ControllerMethodDefinition, DateTime> used = new Dictionary<ControllerMethodDefinition, DateTime>();
                foreach(var c in _manager.Controllers) {
                    foreach (var m in c.Methods) {
                        used.Add(m, DateTime.MinValue);
                    }
                }
                _manager.CommandUsed.Add(args.Channel, used);
            };
            _client.Bot.OnMessageReceived += (object sender, OnMessageReceivedArgs args) =>
            {
                var controllers = _manager.Controllers.Where(x =>
                    (x.Channels.Any(m => m == "any") || x.Channels.Any(m => m == args.ChatMessage.Channel.ToLower()))
                && (x.Users.Any(m => m == "any") || x.Users.Any(m => m == args.ChatMessage.Username))
                && (!x.IsForMod || (x.IsForMod && args.ChatMessage.IsModerator))
                && (!x.IsForVips || (x.IsForVips && args.ChatMessage.IsVip))
                && (!x.IsForSubscriber || (x.IsForSubscriber && args.ChatMessage.IsSubscriber && (x.MinSubDate == 0 || x.MinSubDate >= args.ChatMessage.SubscribedMonthCount)))
                );
                var methods = new Dictionary<ControllerMethodDefinition, ControllerDefinition>();
                foreach (var c in controllers)
                {
                    foreach (var m in c.Methods)
                    { 
                        bool isStartWith = false;
                        if (m.StartWith != null)
                        {
                            string searchString = args.ChatMessage.Message;
                            if (!m.StartWithRegiterCheck)
                                searchString = searchString.ToLower();

                            if ((m.StartWithIsFullWord && searchString.Split(' ')[0] == m.StartWith) ||
                               ((!m.StartWithIsFullWord && searchString.StartsWith(m.StartWith))))
                                isStartWith = true;

                        }
                        else
                        {
                            isStartWith = true;
                        }

                        bool isContains = false;

                        if (m.Contains != null)
                        {
                            string searchString = args.ChatMessage.Message;
                            if (!m.ContainsRegiterCheck)
                                searchString = searchString.ToLower();

                            if (searchString.Contains(m.Contains))
                                isContains = true;
                        }
                        else
                        {
                            isContains = true;
                        }

                        bool isSame = false;

                        if (m.Same != null)
                        {
                            string searchString = args.ChatMessage.Message;
                            if (!m.SameRegisterCheck)
                                searchString = searchString.ToLower();

                            if (searchString == m.Same)
                                isSame = true;
                        }
                        else
                        {
                            isSame = true;
                        }
                        if ((m.Channels.Any(m => m == "any") || m.Channels.Any(m => m == args.ChatMessage.Channel.ToLower()))
                         && (m.Users.Any(m => m == "any") || m.Users.Any(m => m == args.ChatMessage.Username))
                         && (!m.IsForMod || (m.IsForMod && args.ChatMessage.IsModerator))
                         && (!m.IsForVips || (m.IsForVips && args.ChatMessage.IsVip))
                         && (!m.IsForSubscriber ||
                            (m.IsForSubscriber && args.ChatMessage.IsSubscriber && (m.MinSubDate == 0 || m.MinSubDate <= args.ChatMessage.SubscribedMonthCount)))
                         && isStartWith
                         && isContains
                         && isSame)
                        {
                            methods.Add(m, c);
                        }
                    }
                }
                var singleMethod = methods.FirstOrDefault(x => x.Key.IsSingle);
                

                if (!singleMethod.Equals(default(KeyValuePair<ControllerMethodDefinition, ControllerDefinition>)))
                {
                    ExecuteControllerMethod(singleMethod.Key, singleMethod.Value, args.ChatMessage);
                    return;
                }

                foreach (var method in methods)
                {
                    ExecuteControllerMethod(method.Key, method.Value, args.ChatMessage);
                }
            };
            return _client;
        }
        private void ExecuteControllerMethod(ControllerMethodDefinition methodDefinition, ControllerDefinition controllerDefinition, ChatMessage message)
        {
            if (DateTime.Now < _manager.CommandUsed[message.Channel][methodDefinition].AddSeconds(methodDefinition.CoolDown))
                return;

            _manager.CommandUsed[message.Channel][methodDefinition] = DateTime.Now;


            var controller = Activator.CreateInstance(controllerDefinition.ControllerType, GetServices(controllerDefinition.ControllerType, message));
            controllerDefinition.ControllerType.GetProperty("Client").SetValue(controller, _client);
            controllerDefinition.ControllerType.GetProperty("Message").SetValue(controller, message);
            methodDefinition.MethodInfo.Invoke(controller, new object[0]);

        }

        public ControllerBuilder RegisterService<T, TSettings, TFactory>(TSettings settings)
         where T : class
         where TFactory : IServiceFactory<T, TSettings>, new()
        {
            if (settings == null)
                throw new ArgumentNullException();

            if (_manager.Services.Any(x => x.ServiceType == typeof(T)))
                throw new ArgumentException("This service is already registered");

            _manager.Services.Add(new Service(typeof(T), typeof(TFactory), settings));

            return this;
        }

        private object[] GetServices(Type controllerType, ChatMessage message)
        {
            ConstructorInfo[] constructors = controllerType.GetConstructors();
            List<object> result = new List<object>();
            //if (constructors.Length == 0)
            //return new object[0];

            bool isServiceFound = false;
            IEnumerable<ConstructorInfo> q = constructors.OrderBy(x => x.GetParameters().Length).ToList();
            foreach (var constructor in q)
            {
                Type[] parameters = constructor.GetParameters().Select(x => x.ParameterType).ToArray();
                foreach (var p in constructor.GetParameters())
                {
                    isServiceFound = true;
                    if (constructor.GetParameters().Where(x => x == p).Count() != 1)
                        break;

                    var service = _manager.Services.FirstOrDefault(x => x.ServiceType == p.ParameterType);
                    if (service == default)
                        break;

                    MethodInfo factoryMethod = service.SerivceFactory.GetMethod("ServiceFactory");
                    result.Add(factoryMethod.Invoke(Activator.CreateInstance(service.SerivceFactory), new object[] { service.ServiceSettings, _client, message }));
                }

            }
            if (result.Count == 0 && isServiceFound)
                throw new ServiceNotFoundException();
            return result.ToArray();
        }
        private void RegisterControllers()
        {
            List<Assembly> assems = AppDomain.CurrentDomain.GetAssemblies().ToList();
            assems.RemoveAll(x => x.FullName.StartsWith("System"));
            List<Type> controllerTypes = new List<Type>();
            foreach (var s in assems)
            {
                controllerTypes.AddRange(s.GetTypes().Where(x => x.BaseType == typeof(Controller)));
            }
            foreach (var t in controllerTypes)
            {
                List<string> channels = new List<string>();
                IEnumerable<ChannelAttribute> channelsAttribute = t.GetCustomAttributes<ChannelAttribute>();
                foreach (var c in channelsAttribute)
                {
                    channels.Add(c.Channel);
                }
                List<string> users = new List<string>();
                IEnumerable<UserAttribute> usersAttribute = t.GetCustomAttributes<UserAttribute>();
                foreach (var u in usersAttribute)
                {
                    users.Add(u.User);
                }

                //BroadcasterAttribute broadcasterAttribute = t.GetCustomAttribute<BroadcasterAttribute>();
                ModAttribute modAttribute = t.GetCustomAttribute<ModAttribute>();
                VipAttribute vipAttribute = t.GetCustomAttribute<VipAttribute>();
                SubscriberAttribute subscriberAttribute = t.GetCustomAttribute<SubscriberAttribute>();


                var arr = channels.ToArray();
                _manager.Controllers.Add(
                    new ControllerDefinition(t,
                    GetMethods(t),
                    channels.Count == 0 ? new string[] { "any" } : channels.ToArray(),
                    users.Count == 0 ? new string[] { "any" } : users.ToArray(),
                    modAttribute == null ? false : true,
                    vipAttribute == null ? false : true,
                    subscriberAttribute == null ? false : true,
                    subscriberAttribute == null ? 0 : subscriberAttribute.Months
                ));
            }
        }
        private ControllerMethodDefinition[] GetMethods(Type controller)
        {
            IEnumerable<System.Reflection.MethodInfo> allMethods = controller.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public );
            List<ControllerMethodDefinition> methods = new List<ControllerMethodDefinition>();
            foreach (var m in allMethods)
            {
                NonCommandAttribute non = m.GetCustomAttribute<NonCommandAttribute>();
                if (non != null)
                    continue;

                List<string> channels = new List<string>();
                IEnumerable<ChannelAttribute> channelsAttribute = m.GetCustomAttributes<ChannelAttribute>();
                foreach (var c in channelsAttribute)
                {
                    channels.Add(c.Channel);
                }
                List<string> users = new List<string>();
                IEnumerable<UserAttribute> usersAttribute = m.GetCustomAttributes<UserAttribute>();
                foreach (var u in usersAttribute)
                {
                    users.Add(u.User);
                }

                //BroadcasterAttribute broadcasterAttribute = m.GetCustomAttribute<BroadcasterAttribute>();
                ModAttribute modAttribute = m.GetCustomAttribute<ModAttribute>();
                VipAttribute vipAttribute = m.GetCustomAttribute<VipAttribute>();
                SubscriberAttribute subscriberAttribute = m.GetCustomAttribute<SubscriberAttribute>();

                SingleAttribute singleAttribute = m.GetCustomAttribute<SingleAttribute>();
                CoolDownAttribute coolDownAttribute = m.GetCustomAttribute<CoolDownAttribute>();


                StartWithAttribute startWithAttribute = m.GetCustomAttribute<StartWithAttribute>();
                ContainsAttribute containsAttribute = m.GetCustomAttribute<ContainsAttribute>();
                SameAttribute sameAttribute = m.GetCustomAttribute<SameAttribute>();


                var method = new ControllerMethodDefinition(
                    m,
                    channels.Count == 0 ? new string[] { "any" } : channels.ToArray(),
                    users.Count == 0 ? new string[] { "any" } : users.ToArray(),
                    modAttribute == null ? false : true,
                    vipAttribute == null ? false : true,
                    subscriberAttribute == null ? false : true,
                    subscriberAttribute == null ? 0 : subscriberAttribute.Months,

                    containsAttribute == null ? null :
                    containsAttribute.RegisterCheck ?
                    containsAttribute.ContainsString : containsAttribute.ContainsString.ToLower(),

                    containsAttribute == null ? false : containsAttribute.RegisterCheck,

                    startWithAttribute == null ? null :
                    startWithAttribute.RegiterCheck ?
                    startWithAttribute.StartString : startWithAttribute.StartString.ToLower(),

                    startWithAttribute == null ? false : startWithAttribute.IsFullWord,
                    startWithAttribute == null ? false : startWithAttribute.RegiterCheck,
                    singleAttribute == null ? false : true,
                    coolDownAttribute == null ? 0 : coolDownAttribute.CoolDownTime,

                    sameAttribute == null ? null :
                    sameAttribute.RegisterCheck ?
                    sameAttribute.SameString : sameAttribute.SameString.ToLower(),

                    sameAttribute == null ? false : sameAttribute.RegisterCheck
                );
                methods.Add(method);

            }
            return methods.ToArray();
        }
    }
}