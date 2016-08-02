namespace LxUtilities.Definitions.Domain.Messaging
{
    public interface IDomainEventPublisher
    {
        IDomainEventPublisher Publish(IDomainEvent domainEvent);
    }
}