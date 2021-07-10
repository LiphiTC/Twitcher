namespace Twitcher.Controllers.Attributes
{
    [System.AttributeUsage(System.AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public sealed class NonCommandAttribute : System.Attribute
    {
        public NonCommandAttribute()
        { 
        }
    }
}