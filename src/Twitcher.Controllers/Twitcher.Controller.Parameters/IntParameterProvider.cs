using System;
using TwitchLib.Client.Models;

namespace Twitcher.Controllers.Parameters
{
    public class IntParameterProvider : IParameterProvider<int?>
    {
        public int? ParseParameter(string parameter, ChatMessage message)
        {
            int yep;

            if(int.TryParse(parameter, out yep))
                return yep;
            return null; 
        }
    }
}