namespace Hdc.Mercury.Communication
{
    public class AccessChannelConfig
    {
        public uint Interval { get; set; }

        public AccessMode AccessMode { get; set; }

        public SubscriptionMode SubscriptionMode { get; set; }

        public bool IsSimulative { get; set; }
    }
}