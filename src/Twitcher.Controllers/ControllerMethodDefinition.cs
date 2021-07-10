using System;
using System.Collections.Generic;
using System.Reflection;
using Config.Net;
using TwitchLib.Client.Models;

namespace Twitcher.Controllers
{
    public struct ControllerMethodDefinition
    {
        public MethodInfo MethodInfo { get; }
        /// <summary>
        /// Channels where controller can be used
        /// If null, will be used on all connected channels
        /// </summary>
        /// <value></value>
        public string[] Channels { get; }
        /// <summary>
        /// Users, who allowed to use command
        /// If null, will be allowed for all users
        /// </summary>
        /// <value></value>
        public string[] Users { get; }
        public bool IsForMod { get; }
        public bool IsForVips { get; }
        public bool IsForSubscriber { get; }
        /// <summary>
        /// Minimal mounth of sub for user to use controller
        /// If 0, any subscriber
        /// </summary>
        /// <value></value>
        public int MinSubDate { get; }
        public string StartWith { get; }
        public string Contains { get; }
        public bool ContainsRegiterCheck { get; }
        public bool StartWithIsFullWord { get; }
        public bool StartWithRegiterCheck { get; }
        public bool IsSingle { get; }
        public long CoolDown { get; }

        public string Same { get; }
        public bool SameRegisterCheck { get; }

        public ControllerMethodDefinition(MethodInfo method, string[] channels, string[] users,
        bool isForMods, bool isForVips, bool isForSubscriber,
        int minSubDate, string contains, bool containsRegisterCheck,
        string startWith, bool startWithFullWord, bool startWithRegiterCheck,
        bool isSingle, long coolDown, string same, bool sameRegisterCheck)
        {
            MethodInfo = method;
            Channels = channels;
            Users = users;
            IsForMod = isForMods;
            IsForVips = isForVips;
            IsForSubscriber = isForSubscriber;
            MinSubDate = minSubDate;
            StartWith = startWith;
            Contains = contains;
            ContainsRegiterCheck = containsRegisterCheck;
            StartWithIsFullWord = startWithFullWord;
            StartWithRegiterCheck = startWithRegiterCheck;
            IsSingle = isSingle;
            CoolDown = coolDown;
            Same = same;
            SameRegisterCheck = sameRegisterCheck;
        }
    }
}
