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

        modelBuilder.ApplyConfiguration(new CategoriaConfiguration());
        modelBuilder.ApplyConfiguration(new ClienteConfiguration());
        modelBuilder.ApplyConfiguration(new ProductoConfiguration());
        modelBuilder.ApplyConfiguration(new ProformaConfiguration());
        modelBuilder.ApplyConfiguration(new InventarioConfiguration());

        // Configuraciones inline para las que no tienen archivo propio
        modelBuilder.Entity<Ventum>(e => {
            e.ToTable("venta");
            e.HasKey(x => x.Id);
        });

        modelBuilder.Entity<Factura>(e => {
            e.ToTable("factura");
            e.HasKey(x => x.Id);
        });

        modelBuilder.Entity<Detalleventum>(e => {
            e.ToTable("detalleventa");
            e.HasKey(x => x.Id);
        });

        modelBuilder.Entity<Detalleproforma>(e => {
            e.ToTable("detalleproforma");
            e.HasKey(x => x.Id);
        });

        modelBuilder.Entity<Usuario>(e => {
            e.ToTable("usuario");
            e.HasKey(x => x.Id);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}