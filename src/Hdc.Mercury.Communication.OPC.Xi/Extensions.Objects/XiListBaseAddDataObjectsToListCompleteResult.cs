using System;
using System.Collections.Generic;
using Xi.Contracts.Data;

namespace Hdc.Mercury.Communication.OPC.Xi
{
    public class XiListBaseAddDataObjectsToListCompleteResult : EventArgs
    {
        public Exception Error { get; set; }

        public List<AddDataObjectResult> AddDataObjectResults { get; set; }

        public object AsyncState { get; set; }

        public XiListBaseAddDataObjectsToListCompleteResult()
        {
        }

        public XiListBaseAddDataObjectsToListCompleteResult(Exception error,
                                                    List<AddDataObjectResult> addDataObjectResults,
                                                    object asyncState)
        {
            Error = error;
            AddDataObjectResults = addDataObjectResults;
            AsyncState = asyncState;
        }
    }
}