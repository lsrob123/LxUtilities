namespace LxUtilities.Contracts.ServiceBus
{
    public enum BusInteraction
    {
        Unknown = 0,
        Send = 10,
        Publish = 20,
        RequestResponse = 30,
        Saga = 40
    }
}