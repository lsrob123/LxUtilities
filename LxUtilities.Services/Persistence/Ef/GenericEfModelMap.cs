using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Design.PluralizationServices;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using System.Globalization;
using System.Linq.Expressions;
using LxUtilities.Definitions.Core.Domain.Entity;
using LxUtilities.Definitions.Persistence;

namespace LxUtilities.Services.Persistence.EF
{
    public class GenericEfModelMap<TEntity, TRelationalModel> : EntityTypeConfiguration<TRelationalModel>
        where TRelationalModel : GenericRelationalModel<TEntity>
        where TEntity : class, IEntity
    {
        public GenericEfModelMap(string tableName = null)
        {
            if (string.IsNullOrWhiteSpace(tableName))
                tableName = PluralizationService.CreateService(CultureInfo.GetCultureInfo("en-us"))
                    .Pluralize(typeof (TRelationalModel).Name);

            ToTable(tableName);

            HasKey(t => t.Id);
            SetUniqueIndex(t => t.Entity.Key);
        }

        /// <summary>
        ///     Set a property/column to be indexed uniquely
        /// </summary>
        /// <typeparam name="TProperty">Property type, must be a primitive type</typeparam>
        /// <param name="propertyExpression">Expression&lt;Func&lt;T, TProperty&gt;&gt;</param>
        protected void SetUniqueIndex<TProperty>(
            Expression<Func<TRelationalModel, TProperty>> propertyExpression)
            where TProperty : struct
        {
            Property(propertyExpression).IsRequired()
                .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new IndexAttribute {IsUnique = true}));
        }
    }
}