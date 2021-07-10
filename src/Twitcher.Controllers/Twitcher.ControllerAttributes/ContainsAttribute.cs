namespace Twitcher.Controllers.Attributes
{
    [System.AttributeUsage(System.AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public sealed class ContainsAttribute : System.Attribute
    {
        public string ContainsString { get; }
        public bool RegisterCheck { get; set; }
        public ContainsAttribute(string ContainsString)
        {
            this.ContainsString = ContainsString;
        }
    }
}