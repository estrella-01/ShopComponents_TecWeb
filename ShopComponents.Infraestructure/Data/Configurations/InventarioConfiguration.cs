using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopComponents.Core.Entities;

namespace ShopComponents.Infraestructure.Data.Configurations;

public class InventarioConfiguration : IEntityTypeConfiguration<Inventario>
{
    public void Configure(EntityTypeBuilder<Inventario> entity)
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
    }
}