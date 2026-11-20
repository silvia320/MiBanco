using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiBanco.Domain.Entities;

namespace MiBanco.Infrastructure.Persistence.Configurations;

public class TipoCuentaConfiguration : IEntityTypeConfiguration<TipoCuenta>
{
    public void Configure(EntityTypeBuilder<TipoCuenta> b)
    {
        b.ToTable("tipos_cuenta");
        b.HasKey(x => x.Id);
        b.Property(x => x.Nombre).IsRequired().HasMaxLength(60);
        b.HasIndex(x => x.Nombre).IsUnique();
        b.Property(x => x.Descripcion).HasMaxLength(150);
        b.Property(x => x.TasaInteres).HasColumnType("numeric(18,2)");
        b.Property(x => x.CostoMantenimiento).HasColumnType("numeric(18,2)");
    }
}