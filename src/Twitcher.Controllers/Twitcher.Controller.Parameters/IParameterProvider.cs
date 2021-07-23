using System;
using TwitchLib.Client.Models;

namespace Twitcher.Controllers.Parameters
{
    public interface IParameterProvider<T> 
    {
        T ParseParameter(string parameter, ChatMessage message);
    }
}