using ProductosApi.Endpoints;
using ProductosApi.Services;
using Scalar.AspNetCore;
using Microsoft.EntityFrameworkCore;
using ProductosApi.Data;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<ProductosDbContext>(options=> options.UseInMemoryDatabase("ProductosDb"));

builder.Services.AddScoped<IProductoService, ProductoService>();

var app = builder.Build();

app.Services.InicializarBaseDeDatos();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.MapProductosEndpoints();


app.Run();
