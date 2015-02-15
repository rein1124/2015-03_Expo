using Hdc.Patterns;

namespace ODM.Domain.Inspection
{
    public class WorkpieceInfoRemovedDomainEvent : IEvent
    {
        public long Id { get; set; }
    }
}