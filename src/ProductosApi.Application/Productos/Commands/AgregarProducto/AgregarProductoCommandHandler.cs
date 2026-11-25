using ProductosApi.Application.Abstractions;
using ProductosApi.Application.Abstractions.Messaging;
using ProductosApi.Domain.Productos;

namespace ProductosApi.Application.Productos.Commands.AgregarProducto;

internal sealed class AgregarProductoCommandHandler(
    IProductoRepository repository,
    IUnitOfWork unitOfWork ): ICommandHandler<AgregarProductoCommand, ProductoResponse>
{
    public async Task<ProductoResponse> Handle(AgregarProductoCommand command, CancellationToken ct)
    {
        var producto = new Producto { Nombre = command.Nombre };
        repository.Agregar(producto);
        await unitOfWork.SaveChangesAsync(ct);

        return producto.ToResponse();
    }
}