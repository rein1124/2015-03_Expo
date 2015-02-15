using Advosol.Paxi;

namespace Hdc.Mercury.Communication.OPC.Xi
{
    public static class AccessModeExtensions
    {
        public static ListRWAccessMode ToListRWAccessMode(this AccessMode mode)
        {
            switch (mode)
            {
                case AccessMode.Read:
                    return ListRWAccessMode.read;
                case AccessMode.ReadWrite:
                    return ListRWAccessMode.readWrite;
                case AccessMode.Write:
                    return ListRWAccessMode.write;
                default:
                    return ListRWAccessMode.readWrite;
            }
        }

        public static SubscriptionUpdateMode ToSubscriptionUpdateMode(this SubscriptionMode mode)
        {
            switch (mode)
            {
                case SubscriptionMode.Callback:
                    return SubscriptionUpdateMode.callback;
                case SubscriptionMode.None:
                    return SubscriptionUpdateMode.none;
                case SubscriptionMode.Poll:
                    return SubscriptionUpdateMode.poll;
                default:
                    return SubscriptionUpdateMode.callback;
            }
        }
    }
}