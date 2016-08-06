using AutoMapper;
using LxUtilities.Definitions.Mapping;

namespace LxUtilities.Services.Persistence.EF
{
    public class EfModelMapRegister : IMapRegister
    {
        public void RegisterMaps()
        {
            Mapper.Initialize(config=>config.CreateMap());
        }
    }
}