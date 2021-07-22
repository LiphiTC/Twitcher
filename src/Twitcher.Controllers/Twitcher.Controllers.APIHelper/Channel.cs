using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Twitcher.Controllers.Services;
using TwitchLib.Api;
using System.Linq;

namespace Twitcher.Controllers.APIHelper
{
    public class Channel
    {
        public User Broadcaster { get; }
        private TwitchAPI _apiClient;
        public async Task<List<User>> GetUsersAsync() {
            var usersNotParsed = await _apiClient.Undocumented.GetChattersAsync(Broadcaster.UserName);
            if(usersNotParsed.Count == 0) 
                return null;
            
            List<User> users = new List<User>();
            foreach(var u in usersNotParsed) {
                users.Add(new User(_apiClient, u.Username, CreateType.ByUserName));
            }
            return users;
        }
        public Channel(TwitchAPI apiClient, User broadcaster) {
            Broadcaster = broadcaster;
            _apiClient = apiClient;
        }
    }
}