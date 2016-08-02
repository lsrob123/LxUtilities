namespace LxUtilities.Definitions.Core.Domain.Messaging
{
    public interface IDomainEventHandler
    {
        void Handle(IDomainEvent e);
    }
}