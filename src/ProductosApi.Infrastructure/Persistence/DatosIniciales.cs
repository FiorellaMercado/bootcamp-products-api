using ProductosApi.Domain.Productos;

namespace ProductosApi.Infrastructure.Persistence;

internal static class DatosIniciales
{
    internal static void Cargar(ProductosDbContext db)
    {
        if (db.Productos.Any()) return;

        db.Productos.AddRange(
            new Producto {Nombre = "Teclado"},
            new Producto {Nombre = "Mouse"}
        );
        db.SaveChanges();  
    }
}