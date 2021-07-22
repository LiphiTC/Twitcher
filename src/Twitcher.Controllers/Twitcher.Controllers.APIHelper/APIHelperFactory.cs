using System;
using System.Collections.Generic;
using Twitcher.Controllers.Services;
using TwitchLib.Client.Models;
using TwitchLib.Api;

namespace Twitcher.Controllers.APIHelper
{
    public class APIHelperFactory : IServiceFactory<APIHelper, APIHelperSettings>
    {
        public APIHelper ServiceFactory(APIHelperSettings settings, TwitcherClient client, ChatMessage message)
        {
            TwitchAPI api = new TwitchAPI();
            api.Settings.ClientId = settings.ClientID;
            api.Settings.AccessToken = settings.OAToken;
            return new APIHelper(new Channel(api, new User(api, message.Channel, CreateType.ByUserName)), new User(api, message.DisplayName, CreateType.ByDisplayName), api);
        }
    }
}
