namespace ProductosApi.Application.Abstractions;

using ProductosApi.Domain.Productos;

public interface IProductoService
{
    List<Producto> ObtenerTodos();
    Producto ObtenerProducto(int id);
    Producto Agregar(string nombre);
    Producto Actualizar(int id, string nombre);
    void Eliminar(int id);
}