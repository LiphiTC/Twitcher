using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using TwitchLib.Api;

namespace Twitcher.Controllers.APIHelper
{
    public class User
    {
        public string UserName { get; }
        public string DisplayName { get; }
        private string _userID;
        public string UserID
        {
            get
            {
                if (_userID != null)
                    return _userID;

                _userID = _apiClient.Helix.Users.GetUsersAsync(logins: new() { UserName })
                .GetAwaiter().GetResult().Users.First().Id;
                return _userID;
            }
        }
        TwitchAPI _apiClient;

        public async Task<DateTime> GetFollowStartDateAsync(Channel channel)
        {
            var follow = await _apiClient.V5.Users.CheckUserFollowsByChannelAsync(UserID, channel.Broadcaster.UserID);
            return follow.CreatedAt;
        }
        public User(TwitchAPI apiClient, string createInfo, CreateType createType = CreateType.ById)
        {
            _apiClient = apiClient;
            switch (createType)
            {
                case CreateType.ById:
                    _userID = createInfo;
                    break;
                case CreateType.ByUserName:
                    UserName = createInfo;
                    break;
                case CreateType.ByDisplayName:
                    UserName = createInfo.ToLower();
                    DisplayName = createInfo;
                    break;
            }
        }
    }
}