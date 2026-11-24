using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProductosApi.Application.Abstractions;
using ProductosApi.Infrastructure.Persistence;
using ProductosApi.Infrastructure.Services;
using ProductosApi.Application.Abstractions.Data;
using ProductosApi.Infrastructure.ReadModels;


namespace ProductosApi.Infrastructure;


public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string databaseName = "ProductosDb")
    {
        services.AddDbContext<ProductosDbContext>(options => options.UseInMemoryDatabase(databaseName));
        services.AddScoped<IProductoService, ProductoService>();
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