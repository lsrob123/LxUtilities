using AutoMapper;
using LxUtilities.Definitions.Core.Domain.Entity;
using LxUtilities.Definitions.Persistence;

namespace LxUtilities.Services.Persistence
{
    public class RelationalModelToEntityConverter<TRelationalModel, TEntity> : ITypeConverter<TRelationalModel, TEntity>
        where TEntity : class, IEntity
        where TRelationalModel : IRelationalModel<TEntity>
    {
        public TEntity Convert(TRelationalModel source, TEntity destination, ResolutionContext context)
        {
            return source.Entity;
        }
    }
}