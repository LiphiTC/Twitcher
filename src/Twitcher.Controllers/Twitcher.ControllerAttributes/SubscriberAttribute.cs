namespace Twitcher.Controllers.Attributes
{
    [System.AttributeUsage(System.AttributeTargets.Method | System.AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public sealed class SubscriberAttribute : System.Attribute
    {
        public int Months { get; }
        public SubscriberAttribute(int months = 0)
        {
            Months = months;
        }

    }
}