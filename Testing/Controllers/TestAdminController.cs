using Twitcher.Controllers;
using Twitcher.Controllers.Attributes;
using System;
using System.Threading;

namespace Testing.Controllers
{
    [User("liphitc")]
    [User("jstyanxd")]
    public class TestAdminController : Controller
    {
        [StartWith("!анмутлифи")]
        public void Method()
        {
            Send("/timeout LiphiTC 1s");
        }
        [StartWith("!ban")]
        public void Ban()
        {
            string[] splited = Message.Message.Split(' ');
            Send("/ban " + splited[1]);
        }

        [StartWith("!bans")]
        public void BanS()
        {
            string[] splited = Message.Message.Split(' ');
            Send("/ban " + splited[1]);
            Thread.Sleep(1000);
            Send("/unban " + splited[1]);

        }

        [StartWith("!unban")]
        public void UnBan()
        {
            string[] splited = Message.Message.Split(' ');
            Send("/unban " + splited[1]);
        }
        [StartWith("!мут")]
        public void Mute()
        {
            string[] splited = Message.Message.Split(' ');
            Send("/timeout " + splited[1] + ' ' + splited[2]);
        }

    }
}