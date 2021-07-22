using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Twitcher.Controllers.Services;
using TwitchLib.Api;
using System.Linq;

namespace Twitcher.Controllers.APIHelper
{
    public enum CreateType {
        ById,
        ByUserName,
        ByDisplayName
    }
}