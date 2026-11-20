namespace MiBanco.Application.Dtos;

public class TipoCuentaDto
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string? Descripcion { get; set; }
    public decimal TasaInteres { get; set; }
    public decimal CostoMantenimiento { get; set; }

    // Texto que se muestra en los desplegables
    public string Texto => Nombre;
}