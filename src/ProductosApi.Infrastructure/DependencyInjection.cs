using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProductosApi.Application.Abstractions;
using ProductosApi.Infrastructure.Persistence;
using ProductosApi.Infrastructure.Services;
using ProductosApi.Application.Abstractions.Data;
using ProductosApi.Infrastructure.ReadModels;
using ProductosApi.Domain.Productos;


namespace ProductosApi.Infrastructure;


public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string databaseName = "ProductosDb")
    {
        services.AddDbContext<ProductosDbContext>(options => options.UseInMemoryDatabase(databaseName));
        //Escritura
        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ProductosDbContext>());
        services.AddScoped<IProductoRepository, ProductoRepository>();
        
        
        services.AddScoped<IProductoService, ProductoService>();
        
        //Lectura
        services.AddScoped<IProductosReadStore, ProductosReadStore>();
        return services;
    }
    public static void InicializarBaseDeDatos(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ProductosDbContext>();
        db.Database.EnsureCreated();
        DatosIniciales.Cargar(db);
    }
}