namespace MiBanco.Application.Dtos;

public class TransaccionDto
{
    public int Id { get; set; }
    public int CuentaId { get; set; }
    public string? CuentaTexto { get; set; }   // solo lectura, para el listado
    public string Tipo { get; set; } = "D";
    public decimal Monto { get; set; }
    public DateOnly Fecha { get; set; }
    public string Descripcion { get; set; } = string.Empty;
}