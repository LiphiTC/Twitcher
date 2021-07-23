namespace Twitcher.Controllers.Attributes
{
    [System.AttributeUsage(System.AttributeTargets.Method | System.AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public sealed class ChannelAttribute : System.Attribute
    {
        public string Channel { get; }
        public ChannelAttribute(string channel)
        { 
            Channel = channel.ToLower();
        }
    }
}