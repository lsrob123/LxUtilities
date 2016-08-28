using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using LxUtilities.Definitions.Bootstrapping;

namespace LxUtilities.Services.Bootstrapping
{
    public static class Bootstrapper
    {
        private static readonly ConcurrentBag<Action> Actions = new ConcurrentBag<Action>();

        public static void RegisterTasks(params Action[] actions)
        {
            AddBootstrapTasks(actions);
        }

        private static void AddBootstrapTasks(IEnumerable<Action> actions)
        {
            foreach (var action in actions.ToList())
            {
                Actions.Add(action);
            }
        }

        public static void Start()
        {
            var actions = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .SelectMany(type => type.GetMethods())
                .Where(method => method.IsStatic && method.GetCustomAttribute<BootstrapActionAttribute>() != null)
                .Select(method => new Action(() => method.Invoke(null, new object[0])))
                .ToList();

            AddBootstrapTasks(actions);

            foreach (var action in Actions)
            {
                action();
            }
        }
    }
}