using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using LxUtilities.Definitions.Core.Domain.Entity;
using LxUtilities.Definitions.Mapping;
using LxUtilities.Definitions.Persistence;

namespace LxUtilities.Services.Mapping.AutoMapper
{
    public class MappingService : IMappingService
    {
        protected static readonly ConcurrentDictionary<Type, Type> EntityToRelationalModelMaps =
            new ConcurrentDictionary<Type, Type>();

        public TDestination Map<TDestination>(object source)
        {
            return Mapper.Map<TDestination>(source);
        }

        public object Map(object source, Type destinationType)
        {
            return Mapper.Map(source.GetType(), destinationType);
        }

        public IRelationalModel<TEntity> Map<TEntity>(TEntity entity)
            where TEntity : class, IEntity
        {
            var entityType = typeof (TEntity);
            Type relationalModelType;
            if (!EntityToRelationalModelMaps.TryGetValue(entityType, out relationalModelType))
                return null;

            var mappedObject = Map(entity, relationalModelType);
            var relationalModel = mappedObject as IRelationalModel<TEntity>;
            return relationalModel;
        }

        public Type GetRelationalModelType(Type entityType)
        {
            Type relationalModelType;
            return EntityToRelationalModelMaps.TryGetValue(entityType, out relationalModelType)
                ? relationalModelType
                : null;
        }

        public static void AddMaps(IEnumerable<MapSetting> maps)
        {
            AddMapsInternal(maps);
        }

        protected static void AddMapsInternal(IEnumerable<MapSetting> maps)
        {
            Mapper.Initialize(config =>
            {
                config.ForAllMaps((typeMap, expression) =>
                    expression.IgnoreAllSourcePropertiesWithAnInaccessibleSetter());
                foreach (var map in maps.ToList())
                {
                    RegisterMap(config, map.Source, map.Destination, map.CustomMap);
                }
            });
        }

        public static void AddEntityAndRelationalModelMap<TEntity, TRelationalModel>()
            where TEntity : class, IEntity
            where TRelationalModel : IRelationalModel<TEntity>, new()
        {
            var entityType = typeof (TEntity);
            var relationalModelType = typeof (TRelationalModel);

            EntityToRelationalModelMaps.TryAdd(entityType, relationalModelType);
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