namespace Twitcher.Controllers.Attributes
{
    [System.AttributeUsage(System.AttributeTargets.Method | System.AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public sealed class ModAttribute : System.Attribute
    {
        public ModAttribute()
        { }
    }
}