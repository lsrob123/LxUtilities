using System;

namespace LxUtilities.Definitions.Domain
{
    public interface IEntity
    {
        Guid Key { get; }
        void SetKey(Guid key);
    }
}