using System;
using System.Transactions;
using LxUtilities.Definitions.Persistence;

namespace LxUtilities.Services.Persistence
{
    /// <summary>
    /// Provides base class for a generic UnitOfWork factory
    /// </summary>
    /// <typeparam name="T">A UnitOfWork type which implements <see cref="IUnitOfWork"/></typeparam>
    public abstract class UnitOfWorkFactoryBase<T> : IUnitOfWorkFactory<T> where T : IUnitOfWork
    {
        /// <summary>
        /// Abstract method to be implemented to get an instance of the UnitOfWork of the specified generic type
        /// </summary>
        /// <returns></returns>
        protected abstract T GetUnitOfWork();

        /// <summary>
        /// Method for injecting custom action which will be executed within the UnitOfWork's disposable scope, eg. scope with DbContext
        /// </summary>
        /// <param name="action">Custom action</param>
        /// <param name="applyOuterTransactionScope">true: Wrap the UnitOfWork scope with an outer TrasactionScope</param>
        public virtual void Execute(Action<T> action, bool applyOuterTransactionScope = false)
        {
            if (action == null)
                throw new NullReferenceException("action");

            if (applyOuterTransactionScope)
            {
                using (var transactionScope = new TransactionScope())
                {
                    CallActionWithUnitOfWorkScope(action);
                    transactionScope.Complete();
                }
            }
            else
                CallActionWithUnitOfWorkScope(action);
        }

        private void CallActionWithUnitOfWorkScope(Action<T> action)
        {
            using (var unitOfWork = GetUnitOfWork())
            {
                action(unitOfWork);
            }
        }
    }
}
