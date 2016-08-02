using System;
using LxUtilities.Definitions.Domain.Messaging;

namespace LxUtilities.Services.Tests.Domain._ObjectMothers
{
    public class SomePortsService : ISomePortsService
    {
        private readonly IDomainEventService _domainEventService;
        public SomeEntity Data => new SomeEntity(Guid.NewGuid(), Guid.NewGuid());

        public Guid LatestValue { get; protected set; }

        public SomePortsService(IDomainEventService domainEventService)
        {
            _domainEventService = domainEventService;
        }

        public Guid MakeSomeChange()
        {
            var oldValue = Data.SomeValue;
            Data.MakeSomeChange(_domainEventService);
            return oldValue;
        }

        public void ProcessChange(SomeDomainEvent domainEvent)
        {
            LatestValue = domainEvent.Entity.SomeValue;
        }
    }
}