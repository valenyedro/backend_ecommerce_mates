using Application.Interfaces.ICarrito;
using Application.Interfaces.ICarritoProducto;
using Application.Interfaces.ICliente;
using Application.Interfaces.IOrden;
using Application.Interfaces.IProducto;
using Application.UseCase;
using Infraestructure.Command;
using Infraestructure.Persistence;
using Infraestructure.Query;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Custom
var connectionString = builder.Configuration["ConnectionString"];
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(connectionString));

builder.Services.AddScoped<IClienteCommand, ClienteCommand>();
builder.Services.AddScoped<IClienteQuery, ClienteQuery>();
builder.Services.AddScoped<IClienteServices, ClienteServices>();

builder.Services.AddScoped<IProductoCommand, ProductoCommand>();
builder.Services.AddScoped<IProductoQuery, ProductoQuery>();
builder.Services.AddScoped<IProductoServices, ProductoServices>();

builder.Services.AddScoped<ICarritoCommand, CarritoCommand>();
builder.Services.AddScoped<ICarritoQuery, CarritoQuery>();
builder.Services.AddScoped<ICarritoServices, CarritoServices>();

builder.Services.AddScoped<ICarritoProductoCommand, CarritoProductoCommand>();
builder.Services.AddScoped<ICarritoProductoQuery, CarritoProductoQuery>();
builder.Services.AddScoped<ICarritoProductoServices, CarritoProductoServices>();

builder.Services.AddScoped<IOrdenCommand, OrdenCommand>();
builder.Services.AddScoped<IOrdenQuery, OrdenQuery>();
builder.Services.AddScoped<IOrdenServices, OrdenServices>();

builder.Services.AddCors(policy =>
{
    policy.AddDefaultPolicy(options => options.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
