using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;
using ShopComponents.Core.Entities;

namespace ShopComponents.Infraestructure.Data;

public partial class SistemaDbContext : DbContext
{
    public SistemaDbContext()
    {
    }

    public SistemaDbContext(DbContextOptions<SistemaDbContext> options)
        : base(options)
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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;port=3306;database=tecwebdb;uid=root;pwd=1234", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.42-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Categorium>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("categoria");

            entity.Property(e => e.Nombre).HasMaxLength(100);
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("cliente");

            entity.Property(e => e.Ci)
                .HasMaxLength(20)
                .HasColumnName("CI");
            entity.Property(e => e.Direccion).HasMaxLength(200);
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.Telefono).HasMaxLength(20);
        });

        modelBuilder.Entity<Detalleproforma>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("detalleproforma");

            entity.HasIndex(e => e.ProductoId, "ProductoId");

            entity.HasIndex(e => e.ProformaId, "ProformaId");

            entity.Property(e => e.Precio).HasPrecision(10, 2);

            entity.HasOne(d => d.Producto).WithMany(p => p.Detalleproformas)
                .HasForeignKey(d => d.ProductoId)
                .HasConstraintName("detalleproforma_ibfk_2");

            entity.HasOne(d => d.Proforma).WithMany(p => p.Detalleproformas)
                .HasForeignKey(d => d.ProformaId)
                .HasConstraintName("detalleproforma_ibfk_1");
        });

        modelBuilder.Entity<Detalleventum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("detalleventa");

            entity.HasIndex(e => e.ProductoId, "ProductoId");

            entity.HasIndex(e => e.VentaId, "VentaId");

            entity.Property(e => e.Precio).HasPrecision(10, 2);

            entity.HasOne(d => d.Producto).WithMany(p => p.Detalleventa)
                .HasForeignKey(d => d.ProductoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("detalleventa_ibfk_2");

            entity.HasOne(d => d.Venta).WithMany(p => p.Detalleventa)
                .HasForeignKey(d => d.VentaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("detalleventa_ibfk_1");
        });

        modelBuilder.Entity<Factura>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("factura");

            entity.HasIndex(e => e.VentaId, "VentaId");

            entity.Property(e => e.Fecha)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
            entity.Property(e => e.NroFactura).HasMaxLength(50);
            entity.Property(e => e.Total).HasPrecision(10, 2);

            entity.HasOne(d => d.Venta).WithMany(p => p.Facturas)
                .HasForeignKey(d => d.VentaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("factura_ibfk_1");
        });

        modelBuilder.Entity<Inventario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("inventario");

            entity.HasIndex(e => e.ProductoId, "ProductoId");

            entity.Property(e => e.Fecha)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
            entity.Property(e => e.TipoMovimiento).HasMaxLength(50);

            entity.HasOne(d => d.Producto).WithMany(p => p.Inventarios)
                .HasForeignKey(d => d.ProductoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("inventario_ibfk_1");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("producto");

            entity.HasIndex(e => e.CategoriaId, "CategoriaId");

            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.Precio).HasPrecision(10, 2);

            entity.HasOne(d => d.Categoria).WithMany(p => p.Productos)
                .HasForeignKey(d => d.CategoriaId)
                .HasConstraintName("producto_ibfk_1");
        });

        modelBuilder.Entity<Proforma>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("proforma");

            entity.HasIndex(e => e.ClienteId, "ClienteId");

            entity.Property(e => e.Fecha)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
            entity.Property(e => e.Total).HasPrecision(10, 2);

            entity.HasOne(d => d.Cliente).WithMany(p => p.Proformas)
                .HasForeignKey(d => d.ClienteId)
                .HasConstraintName("proforma_ibfk_1");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("usuario");

            entity.HasIndex(e => e.Email, "Email").IsUnique();

            entity.Property(e => e.Activo).HasDefaultValueSql("'1'");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.Password).HasMaxLength(100);
            entity.Property(e => e.Rol).HasMaxLength(50);
        });

        modelBuilder.Entity<Ventum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("venta");

            entity.HasIndex(e => e.ClienteId, "ClienteId");

            entity.HasIndex(e => e.UsuarioId, "UsuarioId");

            entity.Property(e => e.Fecha)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
            entity.Property(e => e.Total).HasPrecision(10, 2);

            entity.HasOne(d => d.Cliente).WithMany(p => p.Venta)
                .HasForeignKey(d => d.ClienteId)
                .HasConstraintName("venta_ibfk_1");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Venta)
                .HasForeignKey(d => d.UsuarioId)
                .HasConstraintName("venta_ibfk_2");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
