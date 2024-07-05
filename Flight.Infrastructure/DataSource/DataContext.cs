using Flight.Domain.Common;
using Flight.Domain.Customers.Entity;
using Flight.Domain.Invoices.Model.Entity;
using Flight.Domain.Products.Entity;
using Microsoft.EntityFrameworkCore;

namespace Flight.Infrastructure.DataSource;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        if (modelBuilder is null)
        {
            return;
        }

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);

        modelBuilder.Entity<Product>();
        modelBuilder.Entity<Customer>();
        modelBuilder.Entity<Invoice>();
        modelBuilder.Entity<ProductInvoice>();

        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            var type = entityType.ClrType;
            if (typeof(DomainEntity).IsAssignableFrom(type))
            {
                modelBuilder.Entity(entityType.Name).Property<DateTime>("CreatedOn");
                modelBuilder.Entity(entityType.Name).Property<DateTime>("LastModifiedOn");
            }
        }

        base.OnModelCreating(modelBuilder);
    }
}

