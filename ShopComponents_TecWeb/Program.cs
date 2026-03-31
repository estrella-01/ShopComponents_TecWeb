using Microsoft.EntityFrameworkCore;
using ShopComponents.Infraestructure.Data;
using ShopComponents.Core.Interfaces;
using ShopComponents.Infrastructure.Repositories;
using ShopComponents.Services.Interfaces;
using ShopComponents.Services.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<IProductoRepository, ProductoRepository>();
builder.Services.AddTransient<IProductoService, ProductoService>();

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