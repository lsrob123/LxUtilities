using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LxUtilities.Definitions.Domain.Messaging;

namespace LxUtilities.Services.Domain.Messaging
{
    public class DomainEventService : IDomainEventService
    {
        protected static readonly Dictionary<Type, List<IDomainEventHandler>> Handlers =
            new Dictionary<Type, List<IDomainEventHandler>>();

        protected static readonly ReaderWriterLockSlim Lock =
            new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);


        public void Publish(IDomainEvent domainEvent)
        {
            var eventType = domainEvent.GetType();
            List<IDomainEventHandler> handlers;
            Lock.EnterReadLock();
            try
            {
                Handlers.TryGetValue(eventType, out handlers);
            }
            finally
            {
                Lock.ExitReadLock();
            }

            if (handlers == null)
                return;

            Parallel.ForEach(handlers, handler => handler.Handle(domainEvent));
        }

        public void Subscribe<TEvent>(IDomainEventHandler handler)
            where TEvent : IDomainEvent
        {
            var eventType = typeof (TEvent);
            Subscribe(eventType, handler);
        }

        public void Subscribe(Type eventType, IDomainEventHandler handler)
        {
            if (!typeof(IDomainEvent).IsAssignableFrom(eventType))
                throw new ArgumentOutOfRangeException(nameof(eventType));

            Lock.EnterUpgradeableReadLock();
            try
            {
                List<IDomainEventHandler> handlers;
                if (!Handlers.TryGetValue(eventType, out handlers))
                {
                    Lock.EnterWriteLock();
                    try
                    {
                        Handlers.Add(eventType, new List<IDomainEventHandler> {handler});
                    }
                    finally
                    {
                        Lock.ExitWriteLock();
                    }
                }
                else if (handlers.All(x => x != handler))
                {
                    Lock.EnterWriteLock();
                    try
                    {
                        handlers.Add(handler);
                    }
                    finally
                    {
                        Lock.ExitWriteLock();
                    }
                }
            }
            finally
            {
                Lock.EnterUpgradeableReadLock();
            }
        }
    }
}