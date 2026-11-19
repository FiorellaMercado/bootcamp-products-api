namespace ProductosApi.Common;

public class NotFoundException(string message) : Exception(message)
{
    public static NotFoundException Producto(int id) => new($"No existe el producto {id}");
}