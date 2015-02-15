using System;
using System.Collections.Generic;
using Advosol.Paxi;
using Xi.Contracts.Data;

namespace Hdc.Mercury.Communication.OPC.Xi
{
    public class OnInformationReportArgs : EventArgs
    {
        public uint listId { get; set; }
        public List<ReadValue> updatedValues { get; set; }
        public List<ErrorInfo> errorInfo { get; set; }
    }
}