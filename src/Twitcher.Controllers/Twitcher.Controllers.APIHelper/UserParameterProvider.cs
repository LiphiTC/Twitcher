using System;
using Twitcher.Controllers.Parameters;
using TwitchLib.Client.Models;

namespace Twitcher.Controllers.APIHelper
{
    public class UserParameterProvider : IParameterProvider<User>
    {
        public User ParseParameter(string parameter, ChatMessage message)
        {

            parameter = parameter.Replace("@", "");
            parameter = parameter.Replace(",", "");
            parameter = parameter.ToLower();
            if (parameter == "me")
                return new User(APIHelperExtension.GlobalAPI, message.UserId, CreateType.ById);
            if (User.CheckUserByUserNameAsync(parameter).GetAwaiter().GetResult())
                return new User(APIHelperExtension.GlobalAPI, parameter, CreateType.ByUserName);
            if (User.CheckUserByUserIDAsync(parameter).GetAwaiter().GetResult())
                return new User(APIHelperExtension.GlobalAPI, parameter, CreateType.ById);
            return null;
        }
    }
}