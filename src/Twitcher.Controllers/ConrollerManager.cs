using System;
using System.Collections.Generic;
using Twitcher.Controllers.Services;

namespace Twitcher.Controllers
{
    sealed class ControllerManager
    {
        public List<ControllerDefinition> Controllers { get; } = new List<ControllerDefinition>();
        public Dictionary<ControllerMethodDefinition, DateTime> LastUsedCommand { get; } = new Dictionary<ControllerMethodDefinition, DateTime>();
        public List<Service> Services { get; } = new List<Service>();
    }
}