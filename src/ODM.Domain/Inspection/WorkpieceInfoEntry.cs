using System;

namespace ODM.Domain.Inspection
{
    public class WorkpieceInfoEntry
    {
        public long Id { get; set; }

        public int Index { get; set; }

        public bool IsReject { get; set; }

        public DateTime? InspectDateTime { get; set; }

        public int IndexOfTotal { get; set; }

        public int IndexOfDay { get; set; }

        public int IndexOfJob { get; set; }
    }
}