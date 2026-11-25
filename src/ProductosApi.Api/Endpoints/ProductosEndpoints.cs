namespace ProductosApi.Endpoints;
using Microsoft.AspNetCore.Http.HttpResults;
using ProductosApi.Models;
using ProductosApi.Domain.Productos;
using ProductosApi.Application.Abstractions;
using ProductosApi.Application.Abstractions.Messaging;
using ProductosApi.Application.Productos.Queries;
using ProductosApi.Application.Productos.Commands.AgregarProducto;
using ProductosApi.Application.Productos;

public static class ProductosEndpoints
{
    public static IEndpointRouteBuilder MapProductosEndpoints(this IEndpointRouteBuilder app)
    {
        var grupo = app.MapGroup("/productos").WithTags("Productos");

        grupo.MapGet("/", async (ISender sender, CancellationToken ct) =>
        {
            return TypedResults.Ok(await sender.Send(new ListarProductosQuery(),ct));
        }).WithName("ListarProductos");

        grupo.MapGet("/{id}", async (int id, ISender sender, CancellationToken ct) =>
        {
            return TypedResults.Ok(await sender.Send(new ObtenerProductoQuery(id), ct));
        }).WithName("ObtenerProducto")
        .ProducesProblem(StatusCodes.Status404NotFound);

        grupo.MapPost("/", async Task<Results<CreatedAtRoute<ProductoResponse>,ValidationProblem>> (AgregarProductoCommand command, ISender sender, CancellationToken ct) =>
        {
            if (string.IsNullOrWhiteSpace(command.Nombre))
            {
                return TypedResults.ValidationProblem( new Dictionary<string, string[]>
                {
                    {"nombre", new[]{"El nombre del producto es obligatorio"}}
                });
            }
            

            
            ProductoResponse productoAgregado = await sender.Send(command, ct);
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