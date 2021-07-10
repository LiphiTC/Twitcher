using System;
using System.Collections.Generic;
using System.Reflection;

namespace Twitcher.Controllers
{
    public struct ControllerDefinition
    {
        public Type ControllerType { get; }
        public string[] Channels { get; }
        public string[] Users { get; }
        public bool IsForMod { get; }
        public bool IsForVips { get; }
        public bool IsForSubscriber { get; }
        /// <summary>
        /// Minimal mounth of sub for user to use controller
        /// If 0, any subscriber
        /// </summary>
        /// <value></value>
        public int MinSubDate { get; }
        public ControllerMethodDefinition[] Methods { get; }
        public ControllerDefinition(Type type, ControllerMethodDefinition[] methodDefinitions, string[] channels, string[] users, bool isForMods, bool isForVips, bool isForSubscriber, int minSubDate)
        {
            ControllerType = type;
            Channels = channels;
            Users = users;
            IsForMod = isForMods;
            IsForVips = isForVips;
            IsForSubscriber = isForSubscriber;
            MinSubDate = minSubDate;
            Methods = methodDefinitions;
        }

    }
}