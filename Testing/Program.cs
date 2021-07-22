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
            .UseTwitchLibProvider(new ConnectionCredentials("LiphiTC", "w08jht8j9nrqfvnieb9c1vsnoyc4me"))
            .JoinChannels(new string[] {
                "safrit22",
                "liphitc",
                "toxynno" })
            .UseControllers()
            .UseJsonHelper("Json")
            .UseAPIHelper("gp762nuuoqcoxypju8c569th9wz7q5", "3vy90gyb0qnunh0ldmfok9428fpvi7")
            .BuildControllers()
            .Connect();

            Console.WriteLine("Hello World!");
            Console.ReadLine();
        }
    }
}
