using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopComponents.Core.Entities;

namespace ShopComponents.Infraestructure.Data.Configurations;

public class ProformaConfiguration : IEntityTypeConfiguration<Proforma>
{
    public void Configure(EntityTypeBuilder<Proforma> entity)
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
    }
}