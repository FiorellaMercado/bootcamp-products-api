using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProductosApi.Application.Abstractions;
using ProductosApi.Infrastructure.Persistence;
using ProductosApi.Infrastructure.Services;

namespace ProductosApi.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string databaseName = "ProductosDb")
    {
        services.AddDbContext<ProductosDbContext>(options => options.UseInMemoryDatabase(databaseName));
        services.AddScoped<IProductoService, ProductoService>();
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