using ProductosApi.Models;
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



interface IProductoService
{
    List<Producto> ObtenerTodos();
    Producto? ObtenerProducto(int id);
    Producto Agregar(string nombre);
    Producto? Actualizar(int id, string nombre);
    bool Eliminar(int id);
}
