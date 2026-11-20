using MiBanco.Domain.Common;

namespace MiBanco.Domain.Entities;

public class Cliente : BaseEntity
{
    public string Cedula { get; set; } = string.Empty;
    public string NombreCompleto { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public int SucursalId { get; set; }
    public bool Activo { get; set; } = true;
    public Sucursal? Sucursal { get; set; }
    public ICollection<Cuenta> Cuentas { get; set; } = new List<Cuenta>();
}