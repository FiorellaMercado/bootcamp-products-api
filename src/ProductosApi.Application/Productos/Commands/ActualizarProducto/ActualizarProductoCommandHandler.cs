using ProductosApi.Application.Abstractions.Messaging;
using ProductosApi.Application.Common;
using ProductosApi.Domain.Productos;

namespace ProductosApi.Application.Productos.Commands.ActualizarProducto;

internal sealed class ActualizarProductoCommandHandler(
    IProductoRepository repository,
    IUnitOfWork unitOfWork
) : ICommandHandler<ActualizarProductoCommand, ProductoResponse>
{
    public async Task<ProductoResponse> Handle(ActualizarProductoCommand command, CancellationToken ct)
    {
        var producto = await repository.ObtenerPorIdAsync(command.Id, ct)
        ?? throw NotFoundException.Producto(command.Id);

        producto.Nombre= command.Nombre;
        await unitOfWork.SaveChangesAsync(ct);

        return producto.ToResponse();
    }
}