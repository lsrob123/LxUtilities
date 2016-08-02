using LxUtilities.Definitions.Core.Domain.Messaging;

namespace LxUtilities.Services.Tests.Domain._ObjectMothers
{
    public class SomeDomainEventHandler : DomainEventHandlerBase<SomeDomainEvent>
    {
        private readonly ISomePortsService _somePortsService;

        public SomeDomainEventHandler(ISomePortsService somePortsService)
        {
            _somePortsService = somePortsService;
        }

        protected override void HandleAction(SomeDomainEvent domainEvent)
        {
            _somePortsService.ProcessChange(domainEvent);
        }
    }
}