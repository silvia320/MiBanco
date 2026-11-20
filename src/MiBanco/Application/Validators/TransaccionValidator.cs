using FluentValidation;
using MiBanco.Application.Abstractions;
using MiBanco.Application.Dtos;
using MiBanco.Domain.Entities;

namespace MiBanco.Application.Validators;

public class TransaccionValidator : AbstractValidator<TransaccionDto>
{
    public TransaccionValidator(IRepository<Cuenta> repoCuenta)
    {
        RuleFor(x => x.CuentaId).Cascade(CascadeMode.Stop)
            .GreaterThan(0).WithMessage("Seleccione Cuenta")
            .MustAsync(async (v, ct) => await repoCuenta.GetByIdAsync(v, ct) is not null)
            .WithMessage("Cuenta: el valor seleccionado no existe");

        RuleFor(x => x.Tipo)
            .Must(v => new[] { "D", "R" }.Contains(v)).WithMessage("Seleccione Tipo");

        RuleFor(x => x.Monto).Cascade(CascadeMode.Stop)
            .GreaterThan(0m).WithMessage("Monto ($) debe ser mayor a 0")
            .LessThanOrEqualTo(1000000m).WithMessage("Monto ($) no puede superar 1000000");

        RuleFor(x => x.Fecha).Cascade(CascadeMode.Stop)
            .Must(f => f >= new DateOnly(1900, 1, 1)).WithMessage("Fecha no es válida")
            .Must(f => f <= DateOnly.FromDateTime(DateTime.Today)).WithMessage("Fecha no puede ser futura");
    }
}