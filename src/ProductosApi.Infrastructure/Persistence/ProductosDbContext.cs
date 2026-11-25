using Microsoft.EntityFrameworkCore;
using ProductosApi.Domain.Productos;

namespace ProductosApi.Infrastructure.Persistence;

internal sealed class ProductosDbContext(DbContextOptions<ProductosDbContext> options) : DbContext(options), IUnitOfWork
{
    public DbSet<Producto> Productos => Set<Producto>();

    protected override void OnModelCreating(ModelBuilder modelBuilder) 
    => modelBuilder.ApplyConfigurationsFromAssembly(typeof (ProductosDbContext).Assembly); 
}