using LxUtilities.Definitions.Bootstrapping;
using LxUtilities.Services.Bootstrapping;

namespace LxUtilities.Services.Tests.Bootstrapping
{
    public class TypeWithAnnotationBootstrap
    {
        [BootstrapAction]
        public static void DoSomething()
        {
            BootstrapState.FlagAnnotated = true;
        }
    }
}