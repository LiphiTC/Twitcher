using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json.Linq;
using Twitcher.Controllers.Services;

namespace Twitcher.Controllers.APIHelper
{
    public static class APIHelperExtension
    {
        public static ControllerBuilder UseAPIHelper(this ControllerBuilder builder, string clientId, string oAuthTken)
        {
            builder.RegisterService<APIHelper, APIHelperSettings, APIHelperFactory>(new APIHelperSettings(clientId, oAuthTken));
            return builder;
        }
    }
}