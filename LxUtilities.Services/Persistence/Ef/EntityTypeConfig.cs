using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Design.PluralizationServices;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using System.Globalization;
using System.Linq.Expressions;
using LxUtilities.Definitions.Core.Domain.Entity;

namespace LxUtilities.Services.Persistence.EF
{
    public class EntityTypeConfig<TEntity> : EntityTypeConfiguration<TEntity>
        where TEntity : class, IEntity
    {
        public EntityTypeConfig(string tableName = null)
        {
            if (string.IsNullOrWhiteSpace(tableName))
                tableName = PluralizationService.CreateService(CultureInfo.GetCultureInfo("en-us"))
                    .Pluralize(typeof (TEntity).Name);

            ToTable(tableName);

            SetUniqueIndex(t => t.Key);
        }

        /// <summary>
        ///     Set a property/column to be indexed uniquely
        /// </summary>
        /// <typeparam name="TProperty">Property type, must be a primitive type</typeparam>
        /// <param name="propertyExpression">Expression&lt;Func&lt;T, TProperty&gt;&gt;</param>
        public EntityTypeConfig<TEntity> SetUniqueIndex<TProperty>(
            Expression<Func<TEntity, TProperty>> propertyExpression)
            where TProperty : struct
        {
            Property(propertyExpression).IsRequired()
                .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new IndexAttribute {IsUnique = true}));

            return this;
        }

        public EntityTypeConfig<TEntity> SetIndex<TProperty>(
            Expression<Func<TEntity, TProperty>> propertyExpression)
            where TProperty : struct
        {
            Property(propertyExpression)
                .HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute[] {}));

            return this;
        }
    }
}