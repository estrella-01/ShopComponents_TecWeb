using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopComponents.Core.Entities;

namespace ShopComponents.Infraestructure.Data.Configurations;

public class ProductoConfiguration : IEntityTypeConfiguration<Producto>
{
    public void Configure(EntityTypeBuilder<Producto> entity)
    {
        entity.HasKey(e => e.Id).HasName("PRIMARY");

        entity.ToTable("producto");

        entity.HasIndex(e => e.CategoriaId, "CategoriaId");

        entity.Property(e => e.Nombre).HasMaxLength(100);
        entity.Property(e => e.Precio).HasPrecision(10, 2);

        entity.HasOne(d => d.Categoria).WithMany(p => p.Productos)
            .HasForeignKey(d => d.CategoriaId)
            .HasConstraintName("producto_ibfk_1");
    }
}