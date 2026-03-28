using Inventory.Domain.Entities;
using Inventory.Domain.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Inventory.infrastructure
{
    public class InventoryDbContext : DbContext
    {

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(AuditableEntity).IsAssignableFrom(entityType.ClrType))
                {
                    var parameter = Expression.Parameter(entityType.ClrType, "e");
                    var property = Expression.Property(parameter, nameof(AuditableEntity.IsDeleted));
                    var notDeleted = Expression.Not(property);
                    var lambda = Expression.Lambda(notDeleted, parameter);

                    modelBuilder.Entity(entityType.ClrType).HasQueryFilter(lambda);
                }
            }
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = Guid.NewGuid();
                        entry.Entity.CreatedAt = DateTime.Now;
                        break;
                    case EntityState.Modified:
                        entry.Entity.UpdatedBy = Guid.NewGuid();
                        entry.Entity.UpdatedAt = DateTime.Now;
                        break;
                    case EntityState.Deleted:
                        entry.Entity.IsDeleted = true;
                        entry.Entity.UpdatedAt = DateTime.Now;
                        entry.Entity.UpdatedBy = Guid.NewGuid();
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }

        public DbSet<Product> Products { get; set; }
    }
}