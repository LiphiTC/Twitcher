using TwitchLib.Client.Models;

namespace Twitcher.Controllers.Services {
    public interface IServiceFactory<T, TSettings>
    where T : class
    {
        T ServiceFactory(TSettings settings, TwitcherClient client, ChatMessage message);
    }
}