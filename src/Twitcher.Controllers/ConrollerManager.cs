using System;
using System.Collections.Generic;
using Twitcher.Controllers.Services;
using Twitcher.Controllers.Parameters;

namespace Twitcher.Controllers
{
    sealed class ControllerManager
    {
        public List<ControllerDefinition> Controllers { get; } = new();
        public Dictionary<string, Dictionary<ControllerMethodDefinition, DateTime>> CommandUsed { get; } = new();
        public List<Service> Services { get; } = new();
        public List<ParameterProvider> ParameterProviders { get; } = new();
    }
}