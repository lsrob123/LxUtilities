using System;

namespace LxUtilities.Definitions.Persistence
{
    /// <summary>
    /// Provides interface for implementing a concrete UnitOfWork
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Persist all changes within the scope. It can be implemented with empty action if it is not required by the backing data store
        /// </summary>
        void SaveChanges();
    }
}