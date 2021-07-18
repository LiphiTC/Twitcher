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
            .UseTwitchLibProvider(new ConnectionCredentials("LiphiTC", "rfx9shas67l3gy289k6mwx0vzrvo71")) 
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
