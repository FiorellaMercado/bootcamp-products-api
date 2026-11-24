using ProductosApi.Application.Abstractions.Data;
using ProductosApi.Application.Abstractions.Messaging;
using ProductosApi.Application.Common;

namespace ProductosApi.Application.Productos.Queries;

public sealed record ObtenerProductoQuery(int Id) : IQuery<ProductoResponse>;

internal sealed class ObtenerProductoQueryHandler(IProductosReadStore readStore)
    : IQueryHandler<ObtenerProductoQuery, ProductoResponse>
{
    public async Task<ProductoResponse> Handle(ObtenerProductoQuery query, CancellationToken ct)
        => await readStore.ObtenerAsync(query.Id, ct) ?? throw NotFoundException.Producto(query.Id);
}