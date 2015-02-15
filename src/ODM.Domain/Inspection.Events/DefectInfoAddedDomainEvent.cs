using Hdc.Patterns;

namespace ODM.Domain.Inspection
{
    public class DefectInfoAddedDomainEvent : IEvent
    {
        public long WorkpieceInfoId { get; set; }

        public DefectInfo DefectInfo { get; set; }
    }
}