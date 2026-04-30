using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopComponents.Core.Entities;

namespace ShopComponents.Infraestructure.Data.Configurations;

public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
{
    public void Configure(EntityTypeBuilder<Cliente> entity)
    {
        entity.HasKey(e => e.Id).HasName("PRIMARY");

        entity.ToTable("cliente");

        entity.Property(e => e.Ci).HasMaxLength(20).HasColumnName("CI");
        entity.Property(e => e.Direccion).HasMaxLength(200);
        entity.Property(e => e.Nombre).HasMaxLength(100);
        entity.Property(e => e.Telefono).HasMaxLength(20);
    }
}