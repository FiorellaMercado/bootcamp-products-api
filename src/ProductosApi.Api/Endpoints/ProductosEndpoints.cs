namespace ProductosApi.Endpoints;
using Microsoft.AspNetCore.Http.HttpResults;
using ProductosApi.Models;
using ProductosApi.Domain.Productos;
using ProductosApi.Application.Abstractions;
public static class ProductosEndpoints
{
    public static IEndpointRouteBuilder MapProductosEndpoints(this IEndpointRouteBuilder app)
    {
        var grupo = app.MapGroup("/productos").WithTags("Productos");

        grupo.MapGet("/", (IProductoService servicio) =>
        {
            return TypedResults.Ok(servicio.ObtenerTodos());
        });

        grupo.MapGet("/{id}", (int id, IProductoService servicio) =>
        {
            return TypedResults.Ok(servicio.ObtenerProducto(id));
        }).WithName("ObtenerProducto")
        .ProducesProblem(StatusCodes.Status404NotFound);

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

        grupo.MapPut("/{id}",Results<Ok<Producto>, ValidationProblem>(int id, ProductoBody body, IProductoService servicio) => 
        {
            string nombre = body.Nombre;

            if (string.IsNullOrWhiteSpace(nombre))
            {
                return TypedResults.ValidationProblem( new Dictionary<string, string[]>
                {
                    {"nombre", new[]{"El nombre del producto es obligatorio"}}
                });
            }
            
            return TypedResults.Ok(servicio.Actualizar(id, nombre));
        }).ProducesProblem(StatusCodes.Status404NotFound);

        grupo.MapDelete("/{id}", (int id, IProductoService servicio)=>{
            
            servicio.Eliminar(id);
            return TypedResults.NoContent();
        }).ProducesProblem(StatusCodes.Status404NotFound);

        return app;

    }
}