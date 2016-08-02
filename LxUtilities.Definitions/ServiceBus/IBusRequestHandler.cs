using LxUtilities.Definitions.ServiceBus.Messges;

namespace LxUtilities.Definitions.ServiceBus
{
    public interface IBusRequestHandler<in TRequest, in TResponse> : IBusMessageHandler<TRequest>
        where TRequest : class, IBusRequest
        where TResponse : class, IBusResponse
    {

    }
}