using System;
using LxUtilities.Definitions.Core.Domain.Entity;
using LxUtilities.Definitions.Core.Domain.Messaging;

namespace LxUtilities.Services.Tests.Domain._ObjectMothers
{
    public class SomeEntity : EntityBase
    {
        public SomeEntity(Guid key, Guid someValue) : base(key)
        {
            SomeValue = someValue;
        }

        public Guid SomeValue { get; private set; }

        public void MakeSomeChange(IDomainEventPublisher domainEventService)
        {
            SomeValue = Guid.NewGuid();
            domainEventService.Publish(new SomeDomainEvent(this));
        }
    }
}