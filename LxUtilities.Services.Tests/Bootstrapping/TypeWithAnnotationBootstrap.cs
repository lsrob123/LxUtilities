using LxUtilities.Definitions.Bootstrapping;

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