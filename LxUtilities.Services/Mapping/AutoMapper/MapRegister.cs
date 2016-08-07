using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LxUtilities.Definitions.Mapping;

namespace LxUtilities.Services.Mapping.AutoMapper
{
    public class MapRegister
    {
        protected ICollection<Action<IMapperConfigurationExpression>> CreateMapActions;

        public MapRegister(params Action<IMapperConfigurationExpression>[] createMapActions)
        {
        }


        public void RegisterMaps()
        {
            Mapper.Initialize(config => config.CreateMap());
        }
    }
}
