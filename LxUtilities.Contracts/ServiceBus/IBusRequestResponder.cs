using LxUtilities.Contracts.ServiceBus.Messges;

namespace LxUtilities.Contracts.ServiceBus
{
    public interface IBusRequestResponder<in TResponse>
        where TResponse : class, IBusResponse
    {
        void Respond(TResponse response);
    }
}