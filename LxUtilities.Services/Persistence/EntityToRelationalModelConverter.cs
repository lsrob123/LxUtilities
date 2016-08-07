using AutoMapper;
using LxUtilities.Definitions.Core.Domain.Entity;
using LxUtilities.Definitions.Persistence;

namespace LxUtilities.Services.Persistence
{
    public class EntityToRelationalModelConverter<TEntity, TRelationalModel> : ITypeConverter<TEntity, TRelationalModel>
        where TEntity : class, IEntity
        where TRelationalModel : IRelationalModel<TEntity>, new()
    {
        public TRelationalModel Convert(TEntity source, TRelationalModel destination, ResolutionContext context)
        {
            var relationalModel = new TRelationalModel();
            relationalModel.SetEntity(source);
            return relationalModel;
        }
    }
}