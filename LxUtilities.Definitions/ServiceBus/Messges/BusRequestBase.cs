using System;

namespace LxUtilities.Definitions.ServiceBus.Messges
{
    public abstract class BusRequestBase<TResponse> : BusRequestBase
        where TResponse : class, IBusResponse
    {
        protected BusRequestBase() : base(typeof (TResponse))
        {
        }
    }

    public abstract class BusRequestBase : IBusRequest
    {
        protected BusRequestBase(Type responseType)
        {
            ResponseType = responseType;
        }

        public Type ResponseType { get; }
    }
}