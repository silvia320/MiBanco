using MiBanco.Domain.Common;

namespace MiBanco.Domain.Entities;

public class Transaccion : BaseEntity
{
    public int CuentaId { get; set; }
    public Cuenta? Cuenta { get; set; }
    public string Tipo { get; set; } = "D";
    public decimal Monto { get; set; }
    public DateOnly Fecha { get; set; }
}