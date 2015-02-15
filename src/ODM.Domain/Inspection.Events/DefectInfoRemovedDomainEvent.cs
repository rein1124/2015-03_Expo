using Hdc.Patterns;

namespace ODM.Domain.Inspection
{
    public class DefectInfoRemovedDomainEvent : IEvent
    {
        public long WorkpieceInfoId { get; set; }

        public long DefectInfoId { get; set; }
    }
}