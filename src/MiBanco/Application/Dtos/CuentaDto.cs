namespace MiBanco.Application.Dtos;

public class CuentaDto
{
    public int Id { get; set; }
    public string NumeroCuenta { get; set; } = string.Empty;
    public DateOnly FechaApertura { get; set; }
    public int ClienteId { get; set; }
    public string? ClienteTexto { get; set; }   // solo lectura, para el listado
    public int TipoCuentaId { get; set; }
    public string? TipoCuentaTexto { get; set; }   // solo lectura, para el listado
    public decimal Saldo { get; set; }

    // Texto que se muestra en los desplegables
    public string Texto => NumeroCuenta;
}