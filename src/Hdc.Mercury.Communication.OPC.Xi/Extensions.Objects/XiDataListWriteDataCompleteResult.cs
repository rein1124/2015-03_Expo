using System;
using System.Collections.Generic;
using Xi.Contracts.Data;

namespace Hdc.Mercury.Communication.OPC.Xi
{
    public class XiDataListWriteDataCompleteResult : EventArgs
    {
        public Exception Error { get; set; }

        public List<AliasResult> AliasResults { get; set; }

        public object AsyncState { get; set; }

        public XiDataListWriteDataCompleteResult()
        {
        }

        public XiDataListWriteDataCompleteResult(Exception error, List<AliasResult> aliasResults, object asyncState = null)
        {
            Error = error;
            AliasResults = aliasResults;
            AsyncState = asyncState;
        }
    }
}