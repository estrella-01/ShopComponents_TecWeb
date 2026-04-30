using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using ShopComponents.Api.Middleware;
using ShopComponents.Core.Interfaces;
using ShopComponents.Infraestructure.Data;
using ShopComponents.Infraestructure.Mappings;
using ShopComponents.Infraestructure.Repositories;
using ShopComponents.Services.Interfaces;
using ShopComponents.Services.Services;
using ShopComponents.Services.Validators;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<SistemaDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))));

builder.Services.AddAutoMapper(typeof(ShopProfile).Assembly);

builder.Services.AddValidatorsFromAssemblyContaining<VentaDtoValidator>();

builder.Services.AddScoped<IDbConnectionFactory, DbConnectionFactory>();
builder.Services.AddScoped<IDapperContext, DapperContext>();

builder.Services.AddScoped<IVentaRepository, VentaRepository>();
builder.Services.AddScoped<IFacturaRepository, FacturaRepository>();
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IVentaService, VentaService>();
builder.Services.AddScoped<IFacturaService, FacturaService>();

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();