using System;
using System.Collections.Generic;
using AutoMapper;
using LxUtilities.Definitions.Core.Domain.Entity;
using LxUtilities.Definitions.Persistence;
using LxUtilities.Services.Mapping.AutoMapper;

namespace LxUtilities.Services.Persistence.EF
{
    public abstract class EfModelMapRegisterBase : MapRegisterBase
    {
        public override void RegisterMaps()
        {
            Mapper.Initialize(config =>
            {
            });
        }

        protected virtual void AddEfModelMaps<TEntity, TRelationalModel>(
            IDictionary<TEntity, TRelationalModel> mapItems)
            where TEntity : class, IEntity
            where TRelationalModel : IRelationalModel<TEntity>;

        {
            
        }
    }
}