
using TwitchLib.Client.Models;

namespace Twitcher.Controllers
{
    public class Controller
    {

        public TwitcherClient Client { get; set; } 
        public ChatMessage Message { get; set; }

        public string MentionPattern { get; } = "@{DisplayName}, ";

        protected void Send(string message)
        {
            Client.Bot.SendMessage(Message.Channel, message);
        }

        protected void SendAnswer(string message)
        {
            Client.Bot.SendMessage(Message.Channel, MentionPattern.Replace("{DisplayName}", Message.DisplayName) + message);
        }
        protected void SendAnswer(string message, string userName)
        {
            Client.Bot.SendMessage(Message.Channel, MentionPattern.Replace("{DisplayName}", userName) + message);
        }
    }

}