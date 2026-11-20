using MiBanco.Domain.Common;

namespace MiBanco.Domain.Entities;

public class TipoCuenta : BaseEntity
{
    public string Nombre { get; set; } = string.Empty;
    public string? Descripcion { get; set; }
    public decimal TasaInteres { get; set; }
    public decimal CostoMantenimiento { get; set; }
    public ICollection<Cuenta> Cuentas { get; set; } = new List<Cuenta>();
}