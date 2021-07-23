namespace Twitcher.Controllers.Attributes {
    [System.AttributeUsage(System.AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public sealed class StartWithAttribute : System.Attribute
    {
        public string StartString { get; }
        public bool RegiterCheck { get; set; }
        public bool IsFullWord { get; set;}
        public StartWithAttribute(string StartString)
        {
            this.StartString = StartString;

        }
    }
}