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
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ShopComponents.Core.CustomEntities;

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

// Password hashing
builder.Services.Configure<PasswordOptions>(
    builder.Configuration.GetSection("PasswordOptions"));
builder.Services.AddSingleton<IPasswordService, PasswordService>();

// Servicios nuevos

builder.Services.AddScoped<IUsuarioService, UsuarioService>();
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

// JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Authentication:Issuer"],
        ValidAudience = builder.Configuration["Authentication:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Authentication:SecretKey"]!))
    };
});

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