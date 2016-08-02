using LxUtilities.Contracts.ServiceBus.Messges;

namespace LxUtilities.Contracts.ServiceBus
{
    public interface IBusRequestHandler<in TRequest, in TResponse> : IBusMessageHandler<TRequest>
        where TRequest : class, IBusRequest
        where TResponse : class, IBusResponse
    {

    }
}