namespace ProductosApi.Services;
using ProductosApi.Models;
using ProductosApi.Data;
using Microsoft.EntityFrameworkCore;

class ProductoService(ProductosDbContext db) : IProductoService
{

    public List<Producto> ObtenerTodos()
    {
        return db.Productos.AsNoTracking().ToList();
    }
    public Producto? ObtenerProducto(int id)
    {
        return db.Productos.Find(id);
    }

    public Producto Agregar(string nombre)
    {
        var producto = new Producto {Nombre = nombre};
        db.Productos.Add(producto);
        db.SaveChanges();
        return producto;
    }

    public Producto? Actualizar(int id, string nombre)
    {
        var producto = db.Productos.Find(id);
        if (producto is null) return null;
        producto.Nombre= nombre;
        db.SaveChanges();
        return producto;
    }

    public bool Eliminar(int id)
    {
        var producto = db.Productos.Find(id);
        if (producto is null) return false;
        db.Productos.Remove(producto);
        db.SaveChanges();
        return true;
    }
}
