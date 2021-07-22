namespace Twitcher.Controllers.APIHelper
{
    public class APIHelperSettings
    {
        public string ClientID { get; }
        public string OAToken { get; }
        public APIHelperSettings(string clientID, string oAToken) {
            ClientID = clientID;
            OAToken = oAToken;
        }
    }
}