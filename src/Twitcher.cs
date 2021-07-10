using System.Collections.Generic;
using TwitchLib.Client;
using TwitchLib.Client.Events;
using TwitchLib.Client.Interfaces;
using TwitchLib.Client.Models;

namespace Twitcher
{
    public class TwitcherClient
    {
        protected internal ITwitchClient Bot { get; private set; }
        private ConnectionCredentials _connectionCredentials;
        List<string> Channels { get; }

        public TwitcherClient()
        {
        }
        /*
        ///Maybe....
        public TwitcherClient UseCustom_botProvider<T>() where T : ITwitchClient, new()
        {
            T _bot = new T();
            _bot.Initialize(_connectionCredentials);
            return this;
        }
        */
        public TwitcherClient UseTwitchLibProvider(ConnectionCredentials credentials)
        {
            _connectionCredentials = credentials;
            Bot = new TwitchClient();
            Bot.Initialize(credentials);
            return this;
        }
        public TwitcherClient JoinChannel(string channel)
        {
            Bot.OnConnected += (object sender, OnConnectedArgs args) =>
            {
                Bot.JoinChannel(channel);
            };
            return this;
        }
        public TwitcherClient JoinChannels(string[] channels)
        {
            Bot.OnConnected += (object sender, OnConnectedArgs args) =>
            {
                foreach (string c in channels)
                {
                    Bot.JoinChannel(c);
                }
            };
            return this;
        }
        public TwitcherClient Connect()
        {
            Bot.Connect();
            return this;
        }
        public TwitcherClient UseHelloMessage(string message)
        {
            Bot.OnJoinedChannel += (object sender, OnJoinedChannelArgs args) => Bot.SendMessage(args.Channel, message);
            return this;
        }

    }
}