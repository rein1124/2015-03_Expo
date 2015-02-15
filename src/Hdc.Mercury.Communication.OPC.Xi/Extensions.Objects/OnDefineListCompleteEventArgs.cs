using System;
using Advosol.Paxi;
using Xi.Contracts.Data;

namespace Hdc.Mercury.Communication.OPC.Xi
{
    public class OnDefineListCompleteEventArgs : EventArgs
    {
        public OnDefineListCompleteEventArgs()
        {
        }

        public OnDefineListCompleteEventArgs(Exception error, ListAttributes rslt, XiDataList dataList, object asyncState = null)
        {
            this.Error = error;
            this.Rslt = rslt;
            this.DataList = dataList;
            this.AsyncState = asyncState;
        }

        public Exception Error { get; set; }

        public ListAttributes Rslt { get; set; }

        public XiDataList DataList { get; set; }

        public object AsyncState { get; set; }
    }
}