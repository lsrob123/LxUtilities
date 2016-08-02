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

        protected abstract void HandleAction(TEvent domainEvent);
    }
}