using System;

namespace Twitcher.Controllers.Services
{
    public class Service
    {
        public Type ServiceType { get; }
        public object ServiceSettings { get; }
        public Type SerivceFactory { get; }
        public Service(Type service, Type factory, object settings)
        {
            ServiceType = service;
            ServiceSettings = settings;
            SerivceFactory = factory;
        }

    }
}