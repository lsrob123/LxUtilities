using System;
using System.Linq;
using AutoMapper;
using LxUtilities.Definitions.Mapping;
using System.Threading;
using System.Collections.Generic;

namespace LxUtilities.Services.Mapping
{
    public class MappingService : IMappingService
    {
        protected static readonly List<MapSetting> Maps =           new List<MapSetting>();

        public static void AddMap(IEnumerable<MapSetting> maps)
        {
            Maps.AddRange(maps);
        }

        public static void AddMap(params MapSetting[] maps)
        {
            Maps.AddRange(maps);
        }

        protected static IMappingExpression RegisterMap(IMapperConfigurationExpression config, 
            Type source, Type destination, Func<IMappingExpression, IMappingExpression> customMapping = null)
        {
            var expression = config.CreateMap(source, destination);

            if (customMapping != null)
                expression = customMapping(expression);

            return expression;
        }

        protected static bool MappingReady;
       

        public MappingService()
        {
            Monitor.Enter(MappingReady);
            try
            {
                if (!MappingReady)
                {
                    Mapper.Initialize(config =>
                    {
                        foreach(var map in Maps)
                        {
                            RegisterMap(config, map.Source, map.Destination, map.CustomMap);
                        }
                    });
                MappingReady = true;
                }
            }
            finally
            {
                Monitor.Exit(MappingReady);
            }
        }

        public TDestination Map<TDestination>(object source)
        {
            return Mapper.Map<TDestination>(source);
        }
    }
}