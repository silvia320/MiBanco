using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiBanco.Domain.Entities;

namespace MiBanco.Infrastructure.Persistence.Configurations;

public class TransaccionConfiguration : IEntityTypeConfiguration<Transaccion>
{
    public void Configure(EntityTypeBuilder<Transaccion> b)
    {
        b.ToTable("transacciones");
        b.HasKey(x => x.Id);
        b.HasOne(x => x.Cuenta)
            .WithMany(t => t.Transacciones)
            .HasForeignKey(x => x.CuentaId)
            .OnDelete(DeleteBehavior.Restrict);
        b.Property(x => x.Tipo).IsRequired().HasMaxLength(1);
        b.Property(x => x.Monto).HasColumnType("numeric(18,2)");
    }
}