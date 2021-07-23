namespace Twitcher.Controllers.Attributes
{
    [System.AttributeUsage(System.AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public sealed class CoolDownAttribute : System.Attribute
    {
        public long CoolDownTime { get; }
        public CoolDownAttribute(long coolDown)
        {
            CoolDownTime = coolDown; 
        }
    }
}