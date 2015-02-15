namespace Hdc.Mercury.Communication
{
    public interface IAccessChannelFactory
    {
        IAccessChannel Create(AccessChannelConfig accessChannelConfig);
    }
}