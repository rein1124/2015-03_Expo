namespace Hdc.Mercury.Communication
{
    public class CommunicationStateArgs : ICommunicationStateArgs<ICommunicationState>
    {
        private readonly ICommunicationState _state;

        public CommunicationStateArgs(ICommunicationState state)
        {
            _state = state;
        }

        public ICommunicationState CommunicationInfo
        {
            get { return _state; }
        }

        public bool IsJustInitialized { get; set; }
    }
}