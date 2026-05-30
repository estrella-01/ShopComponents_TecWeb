using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ShopComponents.Api.Middleware;
using ShopComponents.Core.CustomEntities;
using ShopComponents.Core.Interfaces;
using ShopComponents.Infraestructure.Data;
using ShopComponents.Infraestructure.Repositories;
using ShopComponents.Infrastructure.Repositories;
using ShopComponents.Services.Interfaces;
using ShopComponents.Services.Services;
using ShopComponents.Services.Validators;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Lee variables de entorno (necesario para Azure)
builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
    .AddEnvironmentVariables();

// Base de datos MySQL
builder.Services.AddDbContext<SistemaDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
    ));

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Dapper
builder.Services.AddSingleton<IDbConnectionFactory, DbConnectionFactory>();
builder.Services.AddScoped<IDapperContext, DapperContext>();

// Repositorios
builder.Services.AddScoped<IVentaRepository, VentaRepository>();
builder.Services.AddScoped<IFacturaRepository, FacturaRepository>();
builder.Services.AddScoped<IInventarioRepository, InventarioRepository>();
builder.Services.AddScoped<IProformaRepository, ProformaRepository>();
builder.Services.AddScoped<IProductoRepository, ProductoRepository>();
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IMantenimientoRepository, MantenimientoRepository>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Password hashing
builder.Services.Configure<PasswordOptions>(
    builder.Configuration.GetSection("PasswordOptions"));
builder.Services.AddSingleton<IPasswordService, PasswordService>();

// Servicios
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IVentaService, VentaService>();
builder.Services.AddScoped<IFacturaService, FacturaService>();
builder.Services.AddScoped<IInventarioService, InventarioService>();
builder.Services.AddScoped<IProformaService, ProformaService>();
builder.Services.AddScoped<IProductoService, ProductoService>();
builder.Services.AddScoped<IMantenimientoService, MantenimientoService>();

// Validators
builder.Services.AddScoped<VentaDtoValidator>();
builder.Services.AddScoped<InventarioDtoValidator>();
builder.Services.AddScoped<ProductoValidator>();
builder.Services.AddScoped<FacturaDtoValidator>();
builder.Services.AddScoped<ProformaDtoValidator>();

// JWT Authentication
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

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Swagger con JWT Bearer
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "ShopComponents API",
        Version = "v1",
        Description = "API de gestión de componentes — UCB TecWeb 2025"
    });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Ingresa tu token JWT aquí"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
        options.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

// Swagger visible siempre (necesario para Azure)
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "ShopComponents API v1");
    options.RoutePrefix = string.Empty;
});

app.UseHttpsRedirection();

app.UseAuthentication(); 
app.UseAuthorization();

app.MapControllers();
app.Run();