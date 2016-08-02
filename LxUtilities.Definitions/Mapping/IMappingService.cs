namespace LxUtilities.Definitions.Mapping
{
    public interface IMappingService
    {
        TDestination Map<TDestination>(object source);
    }
}