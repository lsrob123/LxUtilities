using System;

namespace LxUtilities.Definitions.Core.Domain.Entity
{
    public interface IEntity
    {
        Guid Key { get; }
        void SetKey(Guid key);
    }
}