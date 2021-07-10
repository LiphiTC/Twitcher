using System;
using Twitcher;
using Twitcher.Controllers;
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
            .UseTwitchLibProvider(new ConnectionCredentials("liphitc", "YEP")) 
            .JoinChannels(new string[] {
                "safrit22", 
                "liphitc",
                "toxynno" })
            .UseControllers()
            .UseJsonHelper("Json")
            .BuildControllers()
            .Connect(); 

            Console.WriteLine("Hello World!");
            Console.ReadLine();
        }
    }
}
