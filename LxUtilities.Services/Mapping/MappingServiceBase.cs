using AutoMapper;
using LxUtilities.Definitions.Mapping;

namespace LxUtilities.Services.Mapping
{
    public abstract class MappingServiceBase : IMappingService
    {
        protected MappingServiceBase(IMapCreator mapCreator = null)
        {
            mapCreator?.CreateMaps();
        }

        public TDestination Map<TDestination>(object source)
        {
            return Mapper.Map<TDestination>(source);
        }
    }
}