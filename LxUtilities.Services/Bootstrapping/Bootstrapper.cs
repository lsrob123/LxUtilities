using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using LxUtilities.Definitions.Bootstrapping;

namespace LxUtilities.Services.Bootstrapping
{
    public static class Bootstrapper
    {
        private static readonly List<Action> Actions = new List<Action>();

        public static void RegisterTasks(params Action[] actions)
        {
            Actions.AddRange(actions);
        }

        public static void Start()
        {
            var actions = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .SelectMany(type => type.GetMethods())
                .Where(method => method.IsStatic && method.GetCustomAttribute<BootstrapActionAttribute>() != null)
                .Select(method => new Action(() => method.Invoke(null, new object[0])));

            Actions.AddRange(actions);

            foreach (var action in Actions)
            {
                action();
            }
        }
    }
}