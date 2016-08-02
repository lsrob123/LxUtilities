namespace LxUtilities.Definitions.Core.Domain.Messaging
{
    public interface IDomainEventPublisher
    {
        IDomainEventPublisher Publish(IDomainEvent domainEvent);
    }
}