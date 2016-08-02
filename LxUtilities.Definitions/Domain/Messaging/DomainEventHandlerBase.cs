namespace LxUtilities.Definitions.Domain.Messaging
{
    public abstract class DomainEventHandlerBase<TEvent> : IDomainEventHandler
        where TEvent : class, IDomainEvent
    {
        public void Handle(IDomainEvent e)
        {
            var domainEvent = e as TEvent;
            if (domainEvent == null)
                return;

            HandleAction(domainEvent);
        }

        public DomainEventHandlerBase<TEvent> SubscribeWith(IDomainEventSubscriber domainEventSubscriber)
        {
            domainEventSubscriber.Subscribe<TEvent>(this);
            return this;
        }

        protected abstract void HandleAction(TEvent domainEvent);
    }
}