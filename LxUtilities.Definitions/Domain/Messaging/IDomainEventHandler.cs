namespace LxUtilities.Definitions.Domain.Messaging
{
    public interface IDomainEventHandler
    {
        void Handle(IDomainEvent e);
    }

}