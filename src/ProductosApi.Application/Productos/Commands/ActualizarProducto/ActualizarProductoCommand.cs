using ProductosApi.Application.Abstractions.Messaging;

namespace ProductosApi.Application.Productos.Commands.ActualizarProducto;

public sealed record ActualizarProductoCommand(int Id, string Nombre) : ICommand<ProductoResponse>;
