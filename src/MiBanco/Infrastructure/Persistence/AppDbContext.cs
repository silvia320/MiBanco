using Microsoft.EntityFrameworkCore;
using MiBanco.Domain.Entities;

namespace MiBanco.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Sucursal> Sucursales => Set<Sucursal>();
    public DbSet<TipoCuenta> TiposCuenta => Set<TipoCuenta>();
    public DbSet<Cliente> Clientes => Set<Cliente>();
    public DbSet<Cuenta> Cuentas => Set<Cuenta>();
    public DbSet<Transaccion> Transacciones => Set<Transaccion>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        => modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
}