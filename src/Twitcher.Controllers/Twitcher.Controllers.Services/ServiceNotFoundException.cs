namespace Twitcher.Controllers.Services {
    [System.Serializable]
    public class ServiceNotFoundException : System.Exception
    {
        public ServiceNotFoundException() { }
        public ServiceNotFoundException(string message) : base(message) { }
        public ServiceNotFoundException(string message, System.Exception inner) : base(message, inner) { }
        protected ServiceNotFoundException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}