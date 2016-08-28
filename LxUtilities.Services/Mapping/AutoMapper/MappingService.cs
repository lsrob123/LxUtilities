using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using LxUtilities.Definitions.Mapping;

namespace LxUtilities.Services.Mapping.AutoMapper
{
    public class MappingService : IMappingService
    {
        public TDestination Map<TDestination>(object source)
        {
            return Mapper.Map<TDestination>(source);
        }

        public object Map(object source, Type destinationType)
        {
            return Mapper.Map(source.GetType(), destinationType);
        }

        public static void AddMaps(IEnumerable<MapSetting> maps = null)
        {
            AddMapsInternal(maps);
        }

        protected static void AddMapsInternal(IEnumerable<MapSetting> maps)
        {
            Mapper.Initialize(config =>
            {
                config.CreateMissingTypeMaps = true;
                config.ForAllMaps((typeMap, expression) =>
                    expression.IgnoreAllSourcePropertiesWithAnInaccessibleSetter());

                if (maps == null)
                    return;

                foreach (var map in maps.ToList())
                {
                    RegisterMap(config, map.Source, map.Destination, map.CustomMap);
                }
            });
        }

        public static void AddMaps(params MapSetting[] maps)
        {
            AddMapsInternal(maps);
        }

        protected static IMappingExpression RegisterMap(IMapperConfigurationExpression config,
            Type source, Type destination, Func<IMappingExpression, IMappingExpression> customMapping = null)
        {
            var expression = config.CreateMap(source, destination);

            if (customMapping != null)
                expression = customMapping(expression);

            return expression;
        }
    }
}