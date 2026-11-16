using ProductosApi.Models;
using ProductosApi.Services;
using Scalar.AspNetCore;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddSingleton<IProductoService,ProductoService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.MapGet("/productos", (IProductoService servicio) =>
{
    return Results.Ok(servicio.ObtenerTodos());
});

app.MapGet("/productos/{id}", (int id, IProductoService servicio) =>
{
    Producto? producto = servicio.ObtenerProducto(id);
    if (producto == null)
    {
        return Results.NotFound();
    }
    return Results.Ok(producto);
});

app.MapPost("/productos", (ProductoBody producto, IProductoService servicio) =>
{
    if (string.IsNullOrWhiteSpace(producto.Nombre))
    {
        return Results.ValidationProblem( new Dictionary<string, string[]>
        {
            {"nombre", new[]{"El nombre del producto es obligatorio"}}
        });
    }
    
    Producto productoAgregado = servicio.Agregar(producto.Nombre);

    return Results.Created($"/productos/{productoAgregado.Id}",productoAgregado);
});

app.MapPut("/productos/{id}",(int id, ProductoBody producto, IProductoService servicio) => 
{
    string nombre = producto.Nombre;

    if (string.IsNullOrWhiteSpace(nombre))
    {
        return Results.ValidationProblem( new Dictionary<string, string[]>
        {
            {"nombre", new[]{"El nombre del producto es obligatorio"}}
        });
    }
    Producto? productoActualizado= servicio.Actualizar(id, nombre);
    if (productoActualizado == null)
    {
        return Results.NotFound();
    }
    return Results.Ok(productoActualizado);
});

app.MapDelete("/productos/{id}", (int id, IProductoService servicio)=>{
    bool eliminado = servicio.Eliminar(id);
    if (!eliminado)
    {
        return Results.NotFound();
    }
    
    return Results.NoContent();
});




app.Run();
