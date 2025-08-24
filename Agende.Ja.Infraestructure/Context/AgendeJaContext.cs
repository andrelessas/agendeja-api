using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Agende.Ja.Infraestructure.Context
{
    public class AgendeJaContext : DbContext
    {
        public AgendeJaContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var clrType = entityType.ClrType;

                // Verifica se a entidade tem a propriedade pública IsDeleted do tipo bool
                var isDeletedProp = clrType.GetProperty("IsDeleted", BindingFlags.Public | BindingFlags.Instance);
                if (isDeletedProp != null && isDeletedProp.PropertyType == typeof(bool))
                {
                    var parameter = Expression.Parameter(clrType, "e");
                    var propertyAccess = Expression.Property(parameter, isDeletedProp);
                    var falseConstant = Expression.Constant(false, typeof(bool));
                    var body = Expression.Equal(propertyAccess, falseConstant);
                    var lambda = Expression.Lambda(body, parameter);

                    modelBuilder.Entity(clrType).HasQueryFilter(lambda);
                }
            }

            modelBuilder
                .ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            foreach (var foreignKey in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys()))
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict; // Ou DeleteBehavior.SetNull, se preferir
            }

            base.OnModelCreating(modelBuilder);
        }
    }
}
