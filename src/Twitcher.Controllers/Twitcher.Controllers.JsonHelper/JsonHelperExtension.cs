using System;
using System.Collections.Generic;
using Twitcher.Controllers.Services;
using TwitchLib.Client.Models;

namespace Twitcher.Controllers.JsonHelper
{
    public static class JsonHelperExtension
    {
        public static ControllerBuilder UseJsonHelper(this ControllerBuilder builder, string savePath)
        {
            builder.RegisterService<JsonHelper, JsonSettings, JsonHelperFactory>(new JsonSettings(savePath));
            return builder;
        }
    }
}
