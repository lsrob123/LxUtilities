using System;

namespace LxUtilities.Contracts.ServiceBus
{
    public interface ISingleBusControl<out TBus> : IBus
    {
        TBus BusInstance { get; }
        IBusHostConfig Config { get; }
        void Start();
        void Stop();
        Uri GetTypedResponseEndpoint(Type responseType);
    }
}