using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopComponents.Core.Entities;

namespace ShopComponents.Infraestructure.Data.Configurations;

public class CategoriaConfiguration : IEntityTypeConfiguration<Categorium>
{
    public void Configure(EntityTypeBuilder<Categorium> entity)
    {
        entity.HasKey(e => e.Id).HasName("PRIMARY");

        entity.ToTable("categoria");

        entity.Property(e => e.Nombre).HasMaxLength(100);
    }
}