namespace MiBanco.Domain.Common;

// Error de regla de negocio que se puede mostrar al usuario (por ejemplo: "tiene datos relacionados").
public class BusinessRuleException : Exception
{
    public BusinessRuleException(string message) : base(message) { }
}