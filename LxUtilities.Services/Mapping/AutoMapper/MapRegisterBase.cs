using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LxUtilities.Definitions.Mapping;

namespace LxUtilities.Services.Mapping.AutoMapper
{
    public abstract class MapRegisterBase: IMapRegister
    {
        public abstract void RegisterMaps();

        protected virtual IMappingExpression CreateMapingExpression<TSource, TDestination>(IMapperConfigurationExpression config, Func<IMappingExpression, IMappingExpression> customMapping = null)
        {
            return CreateMapingExpression(config, typeof(TSource), typeof(TDestination), customMapping);
        }

        protected virtual IMappingExpression CreateMapingExpression(IMapperConfigurationExpression config, Type source, Type destination, Func<IMappingExpression, IMappingExpression> customMapping=null)
        {
            var expression= config.CreateMap(source, destination);

            if (customMapping != null)
                expression = customMapping(expression);

            return expression;
        }


    }
}
