namespace LxUtilities.Services.Tests.Bootstrapping
{
    public class TypeWithRegisteredBootstrapAction
    {
        public static void DoSomething()
        {
            BootstrapState.FlagRegistered = true;
        }
    }
}