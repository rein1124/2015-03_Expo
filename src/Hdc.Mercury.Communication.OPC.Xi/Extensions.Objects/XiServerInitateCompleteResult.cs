using System;

namespace Hdc.Mercury.Communication.OPC.Xi
{
    public class XiServerInitateCompleteResult : EventArgs
    {
        public Exception Error { get; set; }

        public object AsyncState { get; set; }

        public XiServerInitateCompleteResult()
        {
        }

        public XiServerInitateCompleteResult(Exception error, object asyncState = null)
        {
            Error = error;
            AsyncState = asyncState;
        }
    }
}