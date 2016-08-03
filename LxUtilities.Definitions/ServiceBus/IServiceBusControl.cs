using System;

namespace LxUtilities.Definitions.ServiceBus
{
    public interface IServiceBusControl<out TBus> : IBus
    {
        TBus BusInstance { get; }
        IBusHostConfig Config { get; }
        void Start();
        void Stop();
        Uri GetTypedResponseEndpoint(Type responseType);
    }
}