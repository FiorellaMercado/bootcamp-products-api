using ProductosApi.Models;

namespace ProductosApi.Data;

internal static class DatosIniciales
{
    public static void InicializarBaseDeDatos(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ProductosDbContext>();
        db.Database.EnsureCreated();
        Cargar(db);
    }
    private static void Cargar(ProductosDbContext db)
    {
        if (db.Productos.Any()) return;

        db.Productos.AddRange(
            new Producto {Nombre = "Teclado"},
            new Producto {Nombre = "Mouse"}
        );
        db.SaveChanges();  
    }
}