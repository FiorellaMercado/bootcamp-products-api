namespace ProductosApi.Application.Productos;
using ProductosApi.Domain.Productos;

public record ProductoResponse(int Id, string Nombre);
internal static class ProductoMappings
{
    public static ProductoResponse ToResponse(this Producto p) => new(p.Id, p.Nombre);
}