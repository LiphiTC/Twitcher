using System;
using System.Collections.Generic;
using Twitcher;
using System.Linq;
using Twitcher.Controllers;
using Twitcher.Controllers.Attributes;
using Twitcher.Controllers.JsonHelper;

namespace LiphiBot2.Controllers
{
    [Channel("any")]
    [User("any")]
    public class SafritSubController : Controller
    {
        private readonly JsonHelper _helper;
        public SafritSubController(JsonHelper helper)
        {
            _helper = helper;
        }


        [StartWith("!addsub", IsFullWord = true)]
        [User("LiphiTC")]
        [User("Safrit22")]
        public void AddSub()
        {
            string[] splited = Message.Message.Split(' ');
            if (splited.Length < 2)
            {
                SendAnswer("weirdChamp");
                return;
            }
            float subCount;
            if (float.TryParse(splited[1], out subCount))
            {
                var subs = _helper.GetObject<List<Sub>>("Subs", "Subs");
                if (subs == null)
                    subs = new List<Sub>();

                var userSub = subs.FirstOrDefault(x => x.UserName == splited[2].ToLower());

                if (userSub == null)
                    userSub = new Sub() { UserName = splited[2].ToLower() };

                userSub.SubCount += subCount;
                _helper.EditObject<List<Sub>>("Subs", "Subs", subs);
                SendAnswer("YEP");

            }
            if (float.TryParse(splited[2], out subCount))
            {

            }

        }


    }

    internal class Sub
    {
        public Sub()
        {
        }

        public string UserName { get; set; }
        public float SubCount { get; set; }
    }
}
