using System;
using System.Collections.Generic;
using System.Text;

namespace GrowFood.Domain.Shared
{
    public abstract class AggregateRoot : IHasDomainEvents
    {
        public int Id { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? LastChangeDate { get; set; }

        public EntityStatus EntityStatus { get; set; }

        private IList<DomainEvent> Events { get; } = new List<DomainEvent>();

        public void Deactivate() => EntityStatus = EntityStatus.Deactivated;

        public IReadOnlyList<DomainEvent> GetUnCommitedEvents()
        {
            return new List<DomainEvent>(Events);
        }

        public void MarkEventsAsCommited()
        {
            Events.Clear();
        }

        public void Raise(DomainEvent domainEvent)
        {
            domainEvent.AggregateRootId = this.Id;
            domainEvent.OccuredOn = DateTime.Now;
            domainEvent.EventType = domainEvent.GetType().Name;
            Events.Add(domainEvent);
        }
    }
}
