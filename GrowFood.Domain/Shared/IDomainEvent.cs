using System;
using System.Collections.Generic;
using System.Text;

namespace GrowFood.Domain.Shared
{
    public interface IHasDomainEvents
    {
        IReadOnlyList<DomainEvent> GetUnCommitedEvents();
        void MarkEventsAsCommited();
    }

    public abstract class DomainEvent
    {
        public int AggregateRootId { get; set; }

        public Guid EventId { get; set; }

        public DateTime OccuredOn { get; set; }

        public string EventType { get; set; }
    }
}
