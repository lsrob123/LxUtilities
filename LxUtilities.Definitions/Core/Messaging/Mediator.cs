using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LxUtilities.Definitions.Bootstrapping;

namespace LxUtilities.Definitions.Core.Messaging
{
    public class Mediator : IMediator
    {
        protected static IMediator DefaultInstance;

        protected static readonly Dictionary<Type, List<IMessageHandler>> Handlers =
            new Dictionary<Type, List<IMessageHandler>>();

        protected static readonly ReaderWriterLockSlim Lock =
            new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);

        public Mediator()
        {
            DefaultPublishAction(new MediatorReadyEvent());
        }

        public static IMediator Default => DefaultInstance;

        public virtual IMediator Publish(object message)
        {
            return DefaultPublishAction(message);
        }

        public virtual IMediator Subscribe(Type eventType, IMessageHandler handler)
        {
            Lock.EnterUpgradeableReadLock();
            try
            {
                List<IMessageHandler> handlers;
                if (!Handlers.TryGetValue(eventType, out handlers))
                {
                    Lock.EnterWriteLock();
                    try
                    {
                        Handlers.Add(eventType, new List<IMessageHandler> {handler});
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

            return this;
        }

        [BootstrapAction]
        public static void CreateDefaultInstance()
        {
            DefaultInstance = new Mediator();
        }

        protected IMediator DefaultPublishAction(object message)
        {
            var eventType = message.GetType();
            List<IMessageHandler> handlers;
            Lock.EnterReadLock();
            try
            {
                Handlers.TryGetValue(eventType, out handlers);
            }
            finally
            {
                Lock.ExitReadLock();
            }

            if (handlers != null)
                Parallel.ForEach(handlers, handler => handler.Handle(message));

            return this;
        }
    }
}