using System;

namespace LxUtilities.Definitions.Persistence
{
    /// <summary>
    /// Provides base class for a UnitOfWork which implements IDisposable
    /// </summary>
    public abstract class UnitOfWorkBase
    {
        protected volatile bool Disposed;

        /// <summary>
        /// Execute disposal before finalizer
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Execute disposal by looking at flag "disposing" as condition for the execution
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!Disposed)
            {
                if (disposing)
                {
                    DisposingAction();
                }
            }
            Disposed = true;
        }

        /// <summary>
        /// Abstract method to be implemented for including custom resource disposal 
        /// </summary>
        protected abstract void DisposingAction();

        /// <summary>
        /// Abstract method to be implemented for persisting all changes within the scope. It can be implemented with empty action if it is not required by the backing data store
        /// </summary>
        public abstract void SaveChanges();
    }
}