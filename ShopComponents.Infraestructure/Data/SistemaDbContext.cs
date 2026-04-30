using Microsoft.EntityFrameworkCore;
using ShopComponents.Core.Entities;
using ShopComponents.Infraestructure.Data.Configurations;

namespace ShopComponents.Infraestructure.Data;

public partial class SistemaDbContext : DbContext
{
    public SistemaDbContext(DbContextOptions<SistemaDbContext> options) : base(options)
    {
    }

    public virtual DbSet<Categorium> Categoria { get; set; }
    public virtual DbSet<Cliente> Clientes { get; set; }
    public virtual DbSet<Detalleproforma> Detalleproformas { get; set; }
    public virtual DbSet<Detalleventum> Detalleventa { get; set; }
    public virtual DbSet<Factura> Facturas { get; set; }
    public virtual DbSet<Inventario> Inventarios { get; set; }
    public virtual DbSet<Producto> Productos { get; set; }
    public virtual DbSet<Proforma> Proformas { get; set; }
    public virtual DbSet<Usuario> Usuarios { get; set; }
    public virtual DbSet<Ventum> Venta { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        // Cada entidad tiene su propio archivo de configuración en la carpeta Configurations/
        modelBuilder.ApplyConfiguration(new CategoriaConfiguration());
        modelBuilder.ApplyConfiguration(new ClienteConfiguration());
        modelBuilder.ApplyConfiguration(new ProductoConfiguration());
        modelBuilder.ApplyConfiguration(new ProformaConfiguration());
        modelBuilder.ApplyConfiguration(new InventarioConfiguration());

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}