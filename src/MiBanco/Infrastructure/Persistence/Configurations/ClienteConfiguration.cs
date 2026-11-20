using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiBanco.Domain.Entities;

namespace MiBanco.Infrastructure.Persistence.Configurations;

public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
{
    public void Configure(EntityTypeBuilder<Cliente> b)
    {
        b.ToTable("clientes");
        b.HasKey(x => x.Id);
        b.Property(x => x.Cedula).IsRequired().HasMaxLength(10);
        b.HasIndex(x => x.Cedula).IsUnique();
        b.Property(x => x.NombreCompleto).IsRequired().HasMaxLength(120);
        b.Property(x => x.Email).IsRequired().HasMaxLength(120);
        b.HasIndex(x => x.Email).IsUnique();
        b.Property(x => x.Activo).IsRequired().HasDefaultValue(true);
        b.HasOne(x => x.Sucursal)
            .WithMany(t => t.Clientes)
            .HasForeignKey(x => x.SucursalId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}