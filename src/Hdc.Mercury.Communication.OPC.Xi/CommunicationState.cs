using Hdc.Mercury.Communication.OPC.Xi;

namespace Hdc.Mercury.Communication
{
    public class CommunicationState : ICommunicationState
    {
        internal XiCommunicationState _state;

        public int StateCode
        {
            get { return (int) _state; }
        }
    }
}