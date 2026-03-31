using Microsoft.EntityFrameworkCore;
using ShopComponents.Infraestructure.Data;
using ShopComponents.Core.Interfaces;
using ShopComponents.Infrastructure.Repositories;
using ShopComponents.Services.Interfaces;
using ShopComponents.Services.Services;
using ShopComponents.Infrastructure.Mappings;
using FluentValidation;
using FluentValidation.AspNetCore;
using ShopComponents.Services.Validators;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<IProductoRepository, ProductoRepository>();
builder.Services.AddTransient<IProductoService, ProductoService>();
builder.Services.AddAutoMapper(typeof(ProductoProfile).Assembly);
builder.Services.AddValidatorsFromAssemblyContaining<ProductoValidator>();
builder.Services.AddControllers()
    .AddFluentValidation();
builder.Services.AddDbContext<SistemaDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("MySqlConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("MySqlConnection"))
    ));

// 🔌 CONEXIÓN A MYSQL
var connectionString = builder.Configuration.GetConnectionString("MySqlConnection");

builder.Services.AddDbContext<SistemaDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// Controllers
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();