namespace LxUtilities.Definitions.Core.Messaging
{
    public class MediatorLocator
    {
        protected static readonly IMediator MediatorInstance = new Mediator();
        public static IMediator Default => MediatorInstance;
    }
}