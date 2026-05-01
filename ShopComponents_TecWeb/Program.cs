using FluentValidation;
using Microsoft.EntityFrameworkCore;
using ShopComponents.Api.Middleware;
using ShopComponents.Core.Interfaces;
using ShopComponents.Infraestructure.Data;
using ShopComponents.Infraestructure.Repositories;
using ShopComponents.Infrastructure.Repositories;
using ShopComponents.Services.Interfaces;
using ShopComponents.Services.Services;
using ShopComponents.Services.Validators;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<SistemaDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
    ));

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddSingleton<IDbConnectionFactory, DbConnectionFactory>();
builder.Services.AddScoped<IDapperContext, DapperContext>();

builder.Services.AddScoped<IVentaRepository, VentaRepository>();
builder.Services.AddScoped<IFacturaRepository, FacturaRepository>();
builder.Services.AddScoped<IInventarioRepository, InventarioRepository>();
builder.Services.AddScoped<IProformaRepository, ProformaRepository>();
builder.Services.AddScoped<IProductoRepository, ProductoRepository>();
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IVentaService, VentaService>();
builder.Services.AddScoped<IFacturaService, FacturaService>();
builder.Services.AddScoped<IInventarioService, InventarioService>();
builder.Services.AddScoped<IProformaService, ProformaService>();
builder.Services.AddScoped<IProductoService, ProductoService>();

builder.Services.AddScoped<VentaDtoValidator>();
builder.Services.AddScoped<InventarioDtoValidator>();
builder.Services.AddScoped<ProductoValidator>();
builder.Services.AddScoped<FacturaDtoValidator>();
builder.Services.AddScoped<ProformaDtoValidator>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();