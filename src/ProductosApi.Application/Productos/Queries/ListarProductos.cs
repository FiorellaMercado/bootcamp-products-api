using ProductosApi.Application.Abstractions.Data;
using ProductosApi.Application.Abstractions.Messaging;

namespace ProductosApi.Application.Productos.Queries;

public sealed record ListarProductosQuery : IQuery<IReadOnlyList<ProductoResponse>>;

internal sealed class ListarProductosQueryHandler(IProductosReadStore readStore) 
    : IQueryHandler<ListarProductosQuery, IReadOnlyList<ProductoResponse>>
{
    public async Task<IReadOnlyList<ProductoResponse>> Handle(ListarProductosQuery query, CancellationToken ct) 
        => await readStore.ListarAsync(ct);
}