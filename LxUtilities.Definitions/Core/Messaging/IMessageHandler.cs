namespace LxUtilities.Definitions.Core.Messaging
{
    public interface IMessageHandler
    {
        void Handle(object message);
    }
}