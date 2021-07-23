using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using TwitchLib.Api;

namespace Twitcher.Controllers.APIHelper
{
    public class User
    {
        private string _userName;
        public string UserName
        {
            get
            {
                if (_userName != null)
                    return _userName;

                _userName = _apiClient.Helix.Users.GetUsersAsync(ids: new() { _userID })
                .GetAwaiter().GetResult().Users.First().Login;
                return _userName;
            }
        }
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
        public static async Task<bool> CheckUserByUserNameAsync(string name)
        {
            try
            {
                var matches = await APIHelperExtension.GlobalAPI.V5.Users.GetUserByNameAsync(name);
                if (matches.Total == 0)
                    return false;
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static async Task<bool> CheckUserByUserIDAsync(string id)
        {
            try
            {
                var matches = await APIHelperExtension.GlobalAPI.V5.Users.GetUserByIDAsync(id);
                if (matches == null)
                    return false;
                return true;
            }
            catch
            {
                return false;
            }

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
                    _userName = createInfo;
                    break;
                case CreateType.ByDisplayName:
                    _userName = createInfo.ToLower();
                    DisplayName = createInfo;
                    break;
            }
        }
    }
}