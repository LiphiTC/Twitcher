using System;
using TwitchLib.Client.Models;

namespace Twitcher.Controllers.Parameters
{
    public class StringParameterProvider : IParameterProvider<string>
    {
        public string ParseParameter(string parameter, ChatMessage message) => parameter; 

    }
}