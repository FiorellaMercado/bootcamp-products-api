namespace ProductosApi.Services;
using ProductosApi.Models;

class ProductoService : IProductoService
{
    private readonly List<Producto> _productos = new List<Producto> {
        new Producto(1, "Mouse"),
        new Producto(2,"Teclado")
        
    };
    private int _ultimoId = 2;

    public List<Producto> ObtenerTodos()
    {
        return _productos;
    }
    public Producto? ObtenerProducto(int id)
    {
        Producto? producto = _productos.FirstOrDefault(c=> c.Id == id);
        return producto;
    }

    public Producto Agregar(string nombre)
    {
        _ultimoId++;
        Producto producto= new Producto(_ultimoId,nombre);
        _productos.Add(producto);
        return producto;
    }

    public Producto? Actualizar(int id, string nombre)
    {
        int index = _productos.FindIndex(producto => producto.Id == id);
        if (index == -1)
        {
            return null;
        }
        _productos[index] = new Producto(id, nombre);
        return _productos[index];
    }

    public bool Eliminar(int id)
    {
        int eliminados = _productos.RemoveAll(c=> c.Id == id);
        return eliminados > 0;

    }
}
