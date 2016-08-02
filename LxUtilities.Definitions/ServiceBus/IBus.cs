using System;
using System.Threading.Tasks;
using LxUtilities.Definitions.ServiceBus.Messges;

namespace LxUtilities.Definitions.ServiceBus
{
    public interface IBus
    {
        Task SendAsync<TBusMessage>(TBusMessage command, Action<TBusMessage, object> callback = null)
            where TBusMessage : class, IBusMessage;

        Task PublishAsync<TBusMessage>(TBusMessage message, Action<TBusMessage, object> callback = null)
            where TBusMessage : class, IBusMessage;
    }
}