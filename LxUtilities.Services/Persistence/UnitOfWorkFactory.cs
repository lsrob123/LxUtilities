using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LxUtilities.Definitions.Caching;
using LxUtilities.Definitions.Persistence;

namespace LxUtilities.Services.Persistence
{
    public class UnitOfWorkFactory<TDbContext, TUnitOfWork>: IUnitOfWorkFactory
         where TDbContext : DbContext
        where TUnitOfWork: UnitOfWorkBase<TDbContext>
    {
        protected readonly ICache Cache;
        protected readonly string ConnectionString;

        public UnitOfWorkFactory(ICache cache, string connectionString)
        {
            Cache = cache;
            ConnectionString = connectionString;
        }

        public virtual object Execute(Action<TUnitOfWork<TDbContext>> action)
        {
            if (action==null)
                throw new ArgumentNullException(nameof(action));

            var unitOfWork=new TUnitOfWork();
        }
    }
}
