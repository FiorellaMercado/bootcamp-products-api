namespace ProductosApi.Endpoints;
using Microsoft.AspNetCore.Http.HttpResults;
using ProductosApi.Models;
using ProductosApi.Services;
public static class ProductosEndpoints
{
    public static IEndpointRouteBuilder MapProductosEndpoints(this IEndpointRouteBuilder app)
    {
        var grupo = app.MapGroup("/productos").WithTags("Productos");

        grupo.MapGet("/", (IProductoService servicio) =>
        {
            return TypedResults.Ok(servicio.ObtenerTodos());
        });

        grupo.MapGet("/{id}", Results<Ok<Producto>, NotFound> (int id, IProductoService servicio) =>
        {
            Producto? producto = servicio.ObtenerProducto(id);
            if (producto == null)
            {
                return TypedResults.NotFound();
            }
            return TypedResults.Ok(producto);
        }).WithName("ObtenerProducto");

        grupo.MapPost("/", Results<CreatedAtRoute<Producto>,ValidationProblem> (ProductoBody body, IProductoService servicio) =>
        {
            if (string.IsNullOrWhiteSpace(body.Nombre))
            {
                return TypedResults.ValidationProblem( new Dictionary<string, string[]>
                {
                    {"nombre", new[]{"El nombre del producto es obligatorio"}}
                });
            }
            
            Producto productoAgregado = servicio.Agregar(body.Nombre);

            return TypedResults.CreatedAtRoute(productoAgregado,"ObtenerProducto", new {id= productoAgregado.Id});
        });

        grupo.MapPut("/{id}",Results<Ok<Producto>, NotFound, ValidationProblem>(int id, ProductoBody body, IProductoService servicio) => 
        {
            string nombre = body.Nombre;

            if (string.IsNullOrWhiteSpace(nombre))
            {
                return TypedResults.ValidationProblem( new Dictionary<string, string[]>
                {
                    {"nombre", new[]{"El nombre del producto es obligatorio"}}
                });
            }
            Producto? productoActualizado= servicio.Actualizar(id, nombre);
            if (productoActualizado == null)
            {
                return TypedResults.NotFound();
            }
            return TypedResults.Ok(productoActualizado);
        });

        grupo.MapDelete("/{id}", Results<NotFound,NoContent> (int id, IProductoService servicio)=>{
            bool eliminado = servicio.Eliminar(id);
            if (!eliminado)
            {
                return TypedResults.NotFound();
            }
            
            return TypedResults.NoContent();
        });

        return app;

    }
}