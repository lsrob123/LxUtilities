﻿using System;

namespace LxUtilities.Definitions.Domain.Entity
{
    public interface IEntity
    {
        Guid Key { get; }

        void SetKey(Guid key);

        
    }
}