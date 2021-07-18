using System;
using System.Collections.Generic;
using Twitcher.Controllers.Services;

namespace Twitcher.Controllers
{
    sealed class ControllerManager
    {
        public List<ControllerDefinition> Controllers { get; } = new List<ControllerDefinition>();
        public Dictionary<string, Dictionary<ControllerMethodDefinition, DateTime>> CommandUsed { get; } = new Dictionary<string, Dictionary<ControllerMethodDefinition, DateTime>>();
        public List<Service> Services { get; } = new List<Service>();
    }
}