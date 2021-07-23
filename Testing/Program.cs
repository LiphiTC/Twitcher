using System;
using Twitcher;
using Twitcher.Controllers;
using Twitcher.Controllers.APIHelper;
using Twitcher.Controllers.JsonHelper;
using TwitchLib.Client.Models;

namespace Testing
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Environment.Version);
            TwitcherClient d = new TwitcherClient()
            .UseTwitchLibProvider(new ConnectionCredentials("LiphiTC",                                 "gncuwkky0k1d84d6o27rq8pp2pr0v9"))
            .JoinChannels(new string[] {
                "safrit22",
                "LiphiTC",
                "toxynno" })
            .UseControllers()
            .UseJsonHelper("Json")
            .UseAPIHelper("gp762nuuoqcoxypju8c569th9wz7q5", "3vy90gyb0qnunh0ldmfok9428fpvi7")
            .BuildControllers()
            .Connect();
            Console.ReadLine();
        }
    }
}
