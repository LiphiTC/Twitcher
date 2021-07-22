using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json.Linq;
using Twitcher.Controllers.Services;
using TwitchLib.Api;

namespace Twitcher.Controllers.APIHelper
{
    public class APIHelper
    {
        public Channel Channel { get; }
        public User User { get; }
        public TwitchAPI API { get; }
        public APIHelper(Channel channel, User user, TwitchAPI api)
        {
            Channel = channel;
            User = user;
            API = api;
        }
    }
}