namespace Twitcher.Controllers.Attributes
{
    [System.AttributeUsage(System.AttributeTargets.Method | System.AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public sealed class UserAttribute : System.Attribute
    {
        public string User { get; }
        public UserAttribute(string user)
        { 
            User = user.ToLower();
        }
    }
}