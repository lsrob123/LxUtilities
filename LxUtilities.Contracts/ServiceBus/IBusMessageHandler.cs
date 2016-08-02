using LxUtilities.Contracts.ServiceBus.Messges;

namespace LxUtilities.Contracts.ServiceBus
{
    public interface IBusMessageHandler
    {
    }

    public interface IBusMessageHandler<in TBusMessage> : IBusMessageHandler
        where TBusMessage : class, IBusMessage
    {
        void Handle(TBusMessage message, object context = null);
    }
}