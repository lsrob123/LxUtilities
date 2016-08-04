using System;

namespace LxUtilities.Definitions.Persistence
{
    /// <summary>
    /// Provides interface for a generic UnitOfWork factory
    /// </summary>
    /// <typeparam name="T">A UnitOfWork type which implements <see cref="IUnitOfWork"/></typeparam>
    public interface IUnitOfWorkFactory<out T> where T : IUnitOfWork
    {
        /// <summary>
        /// Method for injecting custom action which will be executed within the UnitOfWork's disposable scope, eg. scope with DbContext
        /// </summary>
        /// <param name="action">Custom action</param>
        /// <param name="applyOuterTransactionScope">true: Wrap the UnitOfWork scope with an outer TrasactionScope</param>
        void Execute(Action<T> action, bool applyOuterTransactionScope = false);
    }
}