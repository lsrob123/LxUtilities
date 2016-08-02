using System;
using LxUtilities.Definitions.Domain.Messaging;

namespace LxUtilities.Services.Tests.Domain._ObjectMothers
{
    public class SomePortsService : ISomePortsService
    {
        private readonly IDomainEventService _domainEventService;
        public SomeEntity Data { get; }

        public Guid LatestValue { get; protected set; }

        public SomePortsService(IDomainEventService domainEventService)
        {
            _domainEventService = domainEventService;
            Data=new SomeEntity(Guid.NewGuid(), Guid.NewGuid());
        }

        public Guid MakeSomeChangeAndGetNewValue()
        {
            Data.MakeSomeChange(_domainEventService);
            return Data.SomeValue;
        }

        public void ProcessChange(SomeDomainEvent domainEvent)
        {
            LatestValue = domainEvent.Entity.SomeValue;
        }
    }
}