namespace ProductosApi.Services;
using ProductosApi.Models;
using ProductosApi.Data;
using ProductosApi.Common;
using Microsoft.EntityFrameworkCore;

class ProductoService(ProductosDbContext db) : IProductoService
{

    public List<Producto> ObtenerTodos()
    {
        return db.Productos.AsNoTracking().ToList();
    }
    public Producto ObtenerProducto(int id)
    {
        return db.Productos.Find(id) ?? throw NotFoundException.Producto(id);
    }

    public Producto Agregar(string nombre)
    {
        var producto = new Producto {Nombre = nombre};
        db.Productos.Add(producto);
        db.SaveChanges();
        return producto;
    }

    public Producto Actualizar(int id, string nombre)
    {
        var producto = ObtenerProducto(id);
        producto.Nombre= nombre;
        db.SaveChanges();
        return producto;
    }

    public void Eliminar(int id)
    {
        var producto = ObtenerProducto(id);
        db.Productos.Remove(producto);
        db.SaveChanges();
    }
}
