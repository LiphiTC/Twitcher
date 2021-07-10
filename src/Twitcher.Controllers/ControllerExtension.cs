
namespace Twitcher.Controllers {
    public static class ControllerExtension {
        public static ControllerBuilder UseControllers(this TwitcherClient client) {
            return new ControllerBuilder(client);
        }
        
    }

}