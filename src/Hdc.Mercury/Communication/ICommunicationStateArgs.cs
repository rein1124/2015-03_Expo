namespace Hdc.Mercury.Communication
{
    public interface ICommunicationStateArgs<T> : ICommunicationStateArgs
    {
        T CommunicationInfo { get; }
    }

    public interface ICommunicationStateArgs
    {
        bool IsJustInitialized { get; set; }
    }
}
