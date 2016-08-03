using System;
using LxUtilities.Definitions.Persistence;

namespace LxUtilities.Services.Persistence.Ef
{
    public class UnitOfWorkBase : IUnitOfWork
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}