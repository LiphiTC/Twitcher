using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json.Linq;
using Twitcher.Controllers.Services;
using TwitchLib.Api;

namespace Twitcher.Controllers.APIHelper
{
    public static class APIHelperExtension
    {
        public static TwitchAPI GlobalAPI { get; private set;} = new TwitchAPI();

        public static ControllerBuilder UseAPIHelper(this ControllerBuilder builder, string clientId, string oAuthTken)
        {
            GlobalAPI.Settings.AccessToken = oAuthTken;
            GlobalAPI.Settings.ClientId = clientId;
            builder.RegisterService<APIHelper, APIHelperSettings, APIHelperFactory>(new APIHelperSettings(clientId, oAuthTken));
            builder.RegisterParameterProvider<User, UserParameterProvider>();
            return builder;
        }
    }
}