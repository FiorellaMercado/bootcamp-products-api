using Microsoft.EntityFrameworkCore;
using ProductosApi.Application.Abstractions.Data;
using ProductosApi.Application.Productos;
using ProductosApi.Infrastructure.Persistence;

namespace ProductosApi.Infrastructure.ReadModels;

internal sealed class ProductosReadStore(ProductosDbContext db) : IProductosReadStore
{
    public Task<ProductoResponse?> ObtenerAsync(int id, CancellationToken ct = default)
        => db.Productos.AsNoTracking()
            .Where(p => p.Id == id)
            .Select(p => new ProductoResponse(p.Id, p.Nombre))
            .FirstOrDefaultAsync(ct);
}