using ProductosApi.Application.Productos;

namespace ProductosApi.Application.Abstractions.Data;

public interface IProductosReadStore
{
    /// <returns>null si el producto no existe.</returns>
    Task<ProductoResponse?> ObtenerAsync(int id, CancellationToken ct = default);
}