using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using LxUtilities.Definitions.Core.Domain.Entity;
using LxUtilities.Definitions.Mapping;
using LxUtilities.Definitions.Persistence;
using LxUtilities.Services.Persistence;

namespace LxUtilities.Services.Mapping.AutoMapper
{
    public class MappingService : IMappingService
    {
        protected static readonly ConcurrentBag<MapSetting> Maps = new ConcurrentBag<MapSetting>();

        protected static readonly ConcurrentDictionary<Type, Type> EntityToRelationalModelMaps =
            new ConcurrentDictionary<Type, Type>();

        public MappingService()
        {
            Mapper.Initialize(config =>
            {
                foreach (var map in Maps)
                {
                    RegisterMap(config, map.Source, map.Destination, map.CustomMap);
                }
            });
        }

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

            var relationalModel = Map(entity, relationalModelType) as IRelationalModel<TEntity>;
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
            foreach (var map in maps.ToList())
            {
                Maps.Add(map);
            }
        }

        public static void AddEntityAndRelationalModelMap<TEntity, TRelationalModel>()
            where TEntity : class, IEntity
            where TRelationalModel : IRelationalModel<TEntity>, new()
        {
            var entityType = typeof (TEntity);
            var relationalModelType = typeof (TRelationalModel);

            AddMaps(new MapSetting(entityType, relationalModelType,
                mappingExpression =>
                    mappingExpression.ConvertUsing<EntityToRelationalModelConverter<TEntity, TRelationalModel>>()));

            AddMaps(new MapSetting(relationalModelType, entityType,
                mappingExpression =>
                    mappingExpression.ConvertUsing<RelationalModelToEntityConverter<TRelationalModel, TEntity>>()));

            EntityToRelationalModelMaps.TryAdd(entityType, relationalModelType);
        }

        public static void AddMaps(params MapSetting[] maps)
        {
            AddMapsInternal(maps);
        }

        protected static IMappingExpression RegisterMap(IMapperConfigurationExpression config,
            Type source, Type destination, Func<IMappingExpression, IMappingExpression> customMapping = null)
        {
            config.CreateMap<string, string>();

            var expression = config.CreateMap(source, destination);

            if (customMapping != null)
                expression = customMapping(expression);

            return expression;
        }
    }
}