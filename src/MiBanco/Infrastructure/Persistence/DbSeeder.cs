using Microsoft.EntityFrameworkCore;
using MiBanco.Domain.Entities;

namespace MiBanco.Infrastructure.Persistence;

public static class DbSeeder
{
    public static async Task SeedAsync(AppDbContext db)
    {
        if (!await db.Sucursales.AnyAsync())
        {
            db.Sucursales.AddRange(
                new Sucursal { Nombre = "Matriz Quito", Ciudad = "Quito", Direccion = "Av. Amazonas N24-15 y Colón", Telefono = "022345678" },
                new Sucursal { Nombre = "Guayaquil Centro", Ciudad = "Guayaquil", Direccion = "Av. 9 de Octubre 1020", Telefono = "042567890" },
                new Sucursal { Nombre = "Cuenca Norte", Ciudad = "Cuenca", Direccion = "Av. Ordóñez Lasso y Gil Ramírez", Telefono = "072889900" });
        }

        if (!await db.TiposCuenta.AnyAsync())
        {
            db.TiposCuenta.AddRange(
                new TipoCuenta { Nombre = "Ahorros", Descripcion = "Cuenta de ahorro personal", TasaInteres = 2.5m, CostoMantenimiento = 1.50m },
                new TipoCuenta { Nombre = "Corriente", Descripcion = "Cuenta con chequera", TasaInteres = 0m, CostoMantenimiento = 5m },
                new TipoCuenta { Nombre = "Ahorro programado", Descripcion = "Ahorro con depósitos mensuales", TasaInteres = 4m, CostoMantenimiento = 0m });
        }

        await db.SaveChangesAsync();

        if (!await db.Clientes.AnyAsync())
        {
            var sucursal = await db.Sucursales.OrderBy(s => s.Id).FirstAsync();
            var ahorros = await db.TiposCuenta.OrderBy(t => t.Id).FirstAsync();

            var ana = new Cliente { Cedula = "1712345678", NombreCompleto = "Ana Pérez", Email = "ana.perez@correo.com", Sucursal = sucursal };
            var luis = new Cliente { Cedula = "1798765432", NombreCompleto = "Luis Andrade", Email = "luis.andrade@correo.com", Sucursal = sucursal };

            var cuentaAna = new Cuenta { NumeroCuenta = "2200123456", FechaApertura = new DateOnly(2026, 1, 15), Cliente = ana, TipoCuenta = ahorros };
            cuentaAna.Transacciones.Add(new Transaccion { Tipo = "D", Monto = 500m, Fecha = new DateOnly(2026, 1, 15) });
            cuentaAna.Transacciones.Add(new Transaccion { Tipo = "D", Monto = 250m, Fecha = new DateOnly(2026, 2, 10) });
            cuentaAna.Transacciones.Add(new Transaccion { Tipo = "R", Monto = 120m, Fecha = new DateOnly(2026, 3, 5) });

            var cuentaLuis = new Cuenta { NumeroCuenta = "2200654321", FechaApertura = new DateOnly(2026, 2, 1), Cliente = luis, TipoCuenta = ahorros };
            cuentaLuis.Transacciones.Add(new Transaccion { Tipo = "D", Monto = 1000m, Fecha = new DateOnly(2026, 2, 1) });

            db.Cuentas.AddRange(cuentaAna, cuentaLuis);
            await db.SaveChangesAsync();
        }
    }
}