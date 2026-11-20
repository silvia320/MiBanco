using MiBanco.Domain.Common;

namespace MiBanco.Domain.Entities;

public class Cuenta : BaseEntity
{
    public string NumeroCuenta { get; set; } = string.Empty;
    public DateOnly FechaApertura { get; set; }
    public int ClienteId { get; set; }
    public Cliente? Cliente { get; set; }
    public int TipoCuentaId { get; set; }
    public TipoCuenta? TipoCuenta { get; set; }
    public string Moneda { get; set; } = string.Empty;

    public ICollection<Transaccion> Transacciones { get; set; } = new List<Transaccion>();

    // Calculado: no es columna, se obtiene de las transacciones (hay que cargarlas con Include).
    public decimal Saldo => Transacciones.Sum(t => t.Tipo == "D" ? t.Monto : -t.Monto);
}