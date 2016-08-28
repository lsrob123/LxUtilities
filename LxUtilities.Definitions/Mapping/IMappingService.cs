using System;
using LxUtilities.Definitions.Core.Domain.Entity;
using LxUtilities.Definitions.Persistence;

namespace LxUtilities.Definitions.Mapping
{
    public interface IMappingService
    {
        TDestination Map<TDestination>(object source);

        object Map(object source, Type destinationType);

        //IRelationalModel<TEntity> Map<TEntity>(TEntity entity)
        //    where TEntity : class, IEntity;

        //Type GetRelationalModelType(Type entityType);
    }
}