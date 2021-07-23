using System;

namespace Twitcher.Controllers.Parameters
{
    struct ParameterProvider
    {
        public Type ParameterProviderType { get; }
        public Type ParameterType { get; }
        public ParameterProvider(Type parameterProvider, Type parameter)
        {   
            ParameterProviderType = parameterProvider;
            ParameterType = parameter;
        }
    }
}