using LxUtilities.Definitions.Domain.Messaging;

namespace LxUtilities.Services.Tests.Domain._ObjectMothers
{
    public class SomeDomainEvent : IDomainEvent
    {
        public SomeDomainEvent(SomeEntity entity)
        {
            Entity = entity;
        }

        public SomeEntity Entity { get; }
    }
}