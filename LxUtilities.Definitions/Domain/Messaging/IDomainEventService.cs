using System;

namespace LxUtilities.Definitions.Domain.Messaging
{
    public interface IDomainEventService
    {
        void Publish(IDomainEvent domainEvent);
        void Subscribe<TEvent>(IDomainEventHandler handler) where TEvent : IDomainEvent;
        void Subscribe(Type eventType, IDomainEventHandler handler);
    }
}