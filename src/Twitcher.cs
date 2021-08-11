using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using TwitchLib.Client;
using TwitchLib.Client.Events;
using TwitchLib.Client.Interfaces;
using TwitchLib.Client.Models;
using TwitchLib.Communication.Events;
using Microsoft.Extensions.Logging;

namespace Twitcher
{
    public class TwitcherClient
    {
        public ITwitchClient Bot { get; private set; }
        private ConnectionCredentials _connectionCredentials;
        List<string> Channels { get; }
        public int DisconectCount { get; private set; }
        public List<string> ConnectedChannels = new List<string>();
        private ILogger _logger;
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
            Bot.OnDisconnected += OnDisconnected;
            Bot.AutoReListenOnException = true;
            Bot.OnReconnected += OnReconected;
            Bot.OnError += OnError;
            
            return this;
        }
        public TwitcherClient JoinChannel(string channel)
        {
            if (channel == null)
                throw new ArgumentNullException();
            if (ConnectedChannels.Any(x => x == channel))
                return this;

            ConnectedChannels.Add(channel);
            return this;
        }
        public TwitcherClient JoinChannels(string[] channels)
        {
            if (channels == null)
                throw new ArgumentNullException();
            foreach (string s in channels)
            {
                if (ConnectedChannels.Any(x => x == s))
                    continue;

                ConnectedChannels.Add(s);
            }
            return this;
        }
        public TwitcherClient Connect()
        {
            Bot.Connect();
            foreach (string c in ConnectedChannels)
                Bot.JoinChannel(c);
            _logger?.LogInformation("Twitcher bot connected");
            return this;
        }
        private async void OnDisconnected(object sender, OnDisconnectedEventArgs args)
        {
            _logger?.LogWarning("Bot disconected");
            DisconectCount += 1;
            _logger?.LogInformation("Reconect attempt: " + DisconectCount);
            await Task.Delay(DisconectCount * 1000);
            Connect();
        }
        private void OnReconected(object sender, OnReconnectedEventArgs args)
        {
            _logger?.LogInformation("Bot reconected");
        }
        private void OnError(object sender, OnErrorEventArgs args)
        {
           _logger.LogCritical(args.Exception.ToString());
        }

        public TwitcherClient UseLogger(ILogger logger)
        {
           _logger = logger;
           return this;
        }
        public TwitcherClient UseHelloMessage(string message)
        {
            Bot.OnJoinedChannel += (object sender, OnJoinedChannelArgs args) => Bot.SendMessage(args.Channel, message);
            return this;
        }

    }
}