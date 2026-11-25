using ProductosApi.Application.Abstractions.Messaging;

namespace ProductosApi.Application.Productos.Commands.AgregarProducto;

public sealed record AgregarProductoCommand(string Nombre) : ICommand<ProductoResponse>;