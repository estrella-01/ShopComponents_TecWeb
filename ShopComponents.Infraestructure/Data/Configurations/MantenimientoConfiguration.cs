using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopComponents.Core.Entities;

namespace ShopComponents.Infraestructure.Data.Configurations;

public class MantenimientoConfiguration : IEntityTypeConfiguration<Mantenimiento>
{
    public void Configure(EntityTypeBuilder<Mantenimiento> builder)
    {
        builder.HasKey(e => e.Id).HasName("PRIMARY");
        builder.ToTable("mantenimiento");

        builder.Property(e => e.Id).HasColumnName("id");
        builder.Property(e => e.ClienteId).HasColumnName("cliente_id");
        builder.Property(e => e.Fecha).HasColumnName("fecha");
        builder.Property(e => e.Descripcion)
            .HasColumnName("descripcion")
            .HasMaxLength(500);
        builder.Property(e => e.Costo)
            .HasColumnName("costo")
            .HasColumnType("decimal(10,2)");
        builder.Property(e => e.Estado)
            .HasColumnName("estado")
            .HasMaxLength(50)
            .HasDefaultValue("Pendiente");
        builder.Property(e => e.Observaciones)
            .HasColumnName("observaciones")
            .HasMaxLength(500);

        builder.HasOne(d => d.Cliente)
            .WithMany()
            .HasForeignKey(d => d.ClienteId)
            .HasConstraintName("fk_mantenimiento_cliente");
    }
}
