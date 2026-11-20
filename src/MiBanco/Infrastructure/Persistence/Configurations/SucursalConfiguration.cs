using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiBanco.Domain.Entities;

namespace MiBanco.Infrastructure.Persistence.Configurations;

public class SucursalConfiguration : IEntityTypeConfiguration<Sucursal>
{
    public void Configure(EntityTypeBuilder<Sucursal> b)
    {
        b.ToTable("sucursales");
        b.HasKey(x => x.Id);
        b.Property(x => x.Nombre).IsRequired().HasMaxLength(60);
        b.Property(x => x.Ciudad).IsRequired().HasMaxLength(60);
        b.Property(x => x.Direccion).IsRequired().HasMaxLength(150);
        b.Property(x => x.Telefono).IsRequired().HasMaxLength(15);
        b.Property(x => x.Email).IsRequired().HasMaxLength(120);
    }
}