namespace ProductosApi.Endpoints;
using ProductosApi.Models;
using ProductosApi.Services;
public static class ProductosEndpoints
{
    public static IEndpointRouteBuilder MapProductosEndpoints(this IEndpointRouteBuilder app)
    {
        var grupo = app.MapGroup("/productos").WithTags("Productos");

        grupo.MapGet("/", (IProductoService servicio) =>
        {
            return Results.Ok(servicio.ObtenerTodos());
        });

        grupo.MapGet("/{id}", (int id, IProductoService servicio) =>
        {
            Producto? producto = servicio.ObtenerProducto(id);
            if (producto == null)
            {
                return Results.NotFound();
            }
            return Results.Ok(producto);
        }).WithName("ObtenerProducto");

        grupo.MapPost("/", (ProductoBody body, IProductoService servicio) =>
        {
            if (string.IsNullOrWhiteSpace(body.Nombre))
            {
                return Results.ValidationProblem( new Dictionary<string, string[]>
                {
                    {"nombre", new[]{"El nombre del producto es obligatorio"}}
                });
            }
            
            Producto productoAgregado = servicio.Agregar(body.Nombre);

            return Results.CreatedAtRoute("ObtenerProducto", new {id= productoAgregado.Id}, productoAgregado);
        });

        grupo.MapPut("/{id}",(int id, ProductoBody body, IProductoService servicio) => 
        {
            string nombre = body.Nombre;

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

        grupo.MapDelete("/{id}", (int id, IProductoService servicio)=>{
            bool eliminado = servicio.Eliminar(id);
            if (!eliminado)
            {
                return Results.NotFound();
            }
            
            return Results.NoContent();
        });

        return app;

    }
}