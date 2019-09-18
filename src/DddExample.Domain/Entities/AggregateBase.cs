using System.Collections.Generic;
using DddExample.Common.Ddd;

namespace DddExample.Domain.Entities
{
    public abstract class AggregateBase
    {
        private readonly List<IDomainEvent> _domainEvents = new List<IDomainEvent>();

        protected void RaiseEvent<TDomainEvent>(TDomainEvent domainEvent)
            where TDomainEvent : IDomainEvent
        {
            _domainEvents.Add(domainEvent);
        }

        public IEnumerable<IDomainEvent> GetPendingEvents()
        {
            return _domainEvents;
        }
    }
}
