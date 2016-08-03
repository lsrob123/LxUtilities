using System;
using LxUtilities.Definitions.Core.Domain.Messaging;

namespace LxUtilities.Definitions.Core.Domain.Entity
{
    public interface IEntity
    {
        Guid Key { get; }
        void SetKey(Guid key);

        void RaiseEvent(IDomainEvent domainEvent);
    }
}