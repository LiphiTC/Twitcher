using System;
using System.Collections.Generic;
using Twitcher.Controllers.Services;
using TwitchLib.Client.Models;

namespace Twitcher.Controllers.JsonHelper
{
    public class JsonHelperFactory : IServiceFactory<JsonHelper, JsonSettings>
    {
        public JsonHelper ServiceFactory(JsonSettings settings, TwitcherClient client, ChatMessage message)
        {
            return new JsonHelper(settings.SavePath);
        }
    }
}
