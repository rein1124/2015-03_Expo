using Hdc.Patterns;

namespace ODM.Domain.Inspection
{
    public class WorkpieceInfoAddedDomainEvent : IEvent
    {
        public WorkpieceInfo WorkpieceInfo { get; set; }

//        public long Id { get; set; }
    }
}