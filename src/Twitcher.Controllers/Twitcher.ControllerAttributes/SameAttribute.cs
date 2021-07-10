namespace Twitcher.Controllers.Attributes
{
    [System.AttributeUsage(System.AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public sealed class SameAttribute : System.Attribute
    {
        public string SameString { get; }
        public bool RegisterCheck { get; set; }
        public SameAttribute(string SameString)
        {
            this.SameString = SameString;
        }
    }
}