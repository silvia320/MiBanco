namespace MiBanco.Application.Dtos;

public class ClienteDto
{
    public int Id { get; set; }
    public string Cedula { get; set; } = string.Empty;
    public string NombreCompleto { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public int SucursalId { get; set; }
    public bool Activo { get; set; } = true;
    public string? SucursalTexto { get; set; }   // solo lectura, para el listado

    // Texto que se muestra en los desplegables
    public string Texto => string.Join(" - ", Cedula, NombreCompleto);
}