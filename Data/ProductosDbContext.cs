using Microsoft.EntityFrameworkCore;
using ProductosApi.Models;

namespace ProductosApi.Data;

internal sealed class ProductosDbContext(DbContextOptions<ProductosDbContext> options) : DbContext(options)
{
    public DbSet<Producto> Productos => Set<Producto>();
    
}