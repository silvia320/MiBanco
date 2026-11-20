using MiBanco.Domain.Common;

namespace MiBanco.Domain.Entities;

public class Sucursal : BaseEntity
{
    public string Nombre { get; set; } = string.Empty;
    public string Ciudad { get; set; } = string.Empty;
    public string Direccion { get; set; } = string.Empty;
    public string Telefono { get; set; } = string.Empty;
    public ICollection<Cliente> Clientes { get; set; } = new List<Cliente>();
}