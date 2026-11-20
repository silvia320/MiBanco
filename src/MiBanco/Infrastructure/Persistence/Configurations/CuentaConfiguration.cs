using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiBanco.Domain.Entities;

namespace MiBanco.Infrastructure.Persistence.Configurations;

public class CuentaConfiguration : IEntityTypeConfiguration<Cuenta>
{
    public void Configure(EntityTypeBuilder<Cuenta> b)
    {
        b.ToTable("cuentas");
        b.HasKey(x => x.Id);
        b.Property(x => x.NumeroCuenta).IsRequired().HasMaxLength(10);
        b.HasIndex(x => x.NumeroCuenta).IsUnique();
        b.Property(x => x.Moneda).IsRequired().HasMaxLength(30);
        b.HasOne(x => x.Cliente)
            .WithMany(t => t.Cuentas)
            .HasForeignKey(x => x.ClienteId)
            .OnDelete(DeleteBehavior.Restrict);
        b.HasOne(x => x.TipoCuenta)
            .WithMany(t => t.Cuentas)
            .HasForeignKey(x => x.TipoCuentaId)
            .OnDelete(DeleteBehavior.Restrict);
        b.Ignore(x => x.Saldo);   // calculado: no existe como columna
    }
}