using LxUtilities.Definitions.ServiceBus.Messges;

namespace LxUtilities.Definitions.ServiceBus
{
    public interface IBusRequestResponder<in TResponse>
        where TResponse : class, IBusResponse
    {
        void Respond(TResponse response);
    }
}