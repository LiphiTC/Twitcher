using Twitcher.Controllers;
using Twitcher.Controllers.Attributes;
using System;
using System.Threading;
using Twitcher.Controllers.JsonHelper;
using Twitcher.Controllers.APIHelper;
using System.Collections.Generic;
using System.Linq;

namespace Testing.Controllers
{
    [User("any")]
    public class TestController : Controller
    {
        private readonly JsonHelper _jsonHelper;
        private readonly APIHelper _api;
        public TestController(JsonHelper helper, APIHelper api)
        {
            _jsonHelper = helper;
            _api = api;
        }

        [StartWith("!ойчто", IsFullWord = true)]
        public void Method()
        {
            Send("/timeout " + Message.Username + " 1s");
        }

        [StartWith("!нупиздапизда")]
        public void NuPizda()
        {
            Send("/timeout " + Message.Username + " 30m");
        }
        [StartWith("!sub")]
        [Subscriber]
        public void Sub()
        {
            DateTime openDate = new DateTime(2021, 7, 12, 15, 0, 0);
            TimeSpan leftTime = openDate - DateTime.Now;

            string[] messasge = Message.Message.Split(' ');
            if (messasge.Length == 1)
            {
                SendAnswer($"До откртытия оффлайн саба: {(int)leftTime.TotalDays} дня, {leftTime.Hours} часа PauseChamp");
                return;
            }
            if (messasge[1].StartsWith('@'))
            {
                Send($"{messasge[1]} До откртытия оффлайн саба: {(int)leftTime.TotalDays} дня, {leftTime.Hours} часа PauseChamp");
                return;
            }
            SendAnswer($"До откртытия оффлайн саба: {(int)leftTime.TotalDays} дня, {leftTime.Hours} часа PauseChamp", messasge[1]);
        }

        [Single]
        [Same("WoahBlanket TeaTime", RegisterCheck = true)]
        public void WoahBlanketTeaTime()
        {
            Send("WoahBlanket TeaTime");
        }
        [Single]
        [Same("json", RegisterCheck = true)]
        public void TestJson()
        {
            _jsonHelper.EditObject<string>("test", "test2", "YEP");
        }
        [CoolDown(10)]
        [Same("WoahBlanket", RegisterCheck = true)]
        public void WoahBlanket()
        {
            Send("WoahBlanket");
        }
        [CoolDown(10)]
        [Contains("баллы", RegisterCheck = false)]
        public void Balls()
        {
            SendAnswer("Баллы можно пополнять только во время стрима PETTHEPEPEGA");
        }



        [CoolDown(10)]
        [StartWith("!квест")]
        public void Quest()
        {
            SendAnswer("Подсказку ты найдёшь, в ныне тихом месте. А раньше под прекрасные мелодии, под живописные пейзажи резвились звуки там.");
        }

        [CoolDown(5)]
        [StartWith("!follow")]
        public async void Follow()
        {
            var YEP = await _api.User.GetFollowStartDateAsync(_api.Channel);
            SendAnswer(YEP.ToString());
        }
        [CoolDown(5)]
        [StartWith("!age")]
        public async void Age()
        {
            var YEP = await _api.API.Helix.Users.GetUsersAsync(logins: new List<string>() { Message.Username });
            SendAnswer(YEP.Users.First().CreatedAt.ToString());
        }
    }
}