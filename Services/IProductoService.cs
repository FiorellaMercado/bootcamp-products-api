namespace ProductosApi.Services;
using ProductosApi.Models;

interface IProductoService
{
    List<Producto> ObtenerTodos();
    Producto? ObtenerProducto(int id);
    Producto Agregar(string nombre);
    Producto? Actualizar(int id, string nombre);
    bool Eliminar(int id);
}