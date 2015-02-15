namespace Hdc.Mercury.Communication
{
    public interface IAccessChannelManager
    {
        IAccessChannel GetChannel(string channelName);
    }
}