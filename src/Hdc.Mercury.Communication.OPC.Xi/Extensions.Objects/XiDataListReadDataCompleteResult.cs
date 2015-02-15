using System;
using System.Collections.Generic;
using Advosol.Paxi;

namespace Hdc.Mercury.Communication.OPC.Xi
{
    public class XiDataListReadDataCompleteResult : EventArgs
    {
        public Exception Error { get; set; }

        public List<ReadValue> ReadValues { get; set; }

        public object AsyncState { get; set; }

        public XiDataListReadDataCompleteResult()
        {
        }

        public XiDataListReadDataCompleteResult(Exception error, List<ReadValue> readValues, object asyncState = null)
        {
            Error = error;
            ReadValues = readValues;
            AsyncState = asyncState;
        }
    }
}