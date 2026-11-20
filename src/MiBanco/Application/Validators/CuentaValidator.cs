using FluentValidation;
using MiBanco.Application.Abstractions;
using MiBanco.Application.Dtos;
using MiBanco.Domain.Entities;

namespace MiBanco.Application.Validators;

public class CuentaValidator : AbstractValidator<CuentaDto>
{
    public CuentaValidator(IRepository<Cuenta> self, IRepository<Cliente> repoCliente, IRepository<TipoCuenta> repoTipoCuenta)
    {
        RuleFor(x => x.NumeroCuenta).Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Número de cuenta: campo obligatorio")
            .MaximumLength(10).WithMessage("Número de cuenta: máximo 10 caracteres")
            .Matches(@"^\d{10}$").WithMessage("El número de cuenta debe tener exactamente 10 dígitos")
            .MustAsync(async (dto, v, ct) => !await self.AnyAsync(e => e.NumeroCuenta == v.Trim() && e.Id != dto.Id, ct))
            .WithMessage("Número de cuenta: ya existe un registro con ese valor");

        RuleFor(x => x.FechaApertura).Cascade(CascadeMode.Stop)
            .Must(f => f >= new DateOnly(1900, 1, 1)).WithMessage("Fecha de apertura no es válida")
            .Must(f => f <= DateOnly.FromDateTime(DateTime.Today)).WithMessage("Fecha de apertura no puede ser futura");

        RuleFor(x => x.ClienteId).Cascade(CascadeMode.Stop)
            .GreaterThan(0).WithMessage("Seleccione Cliente")
            .MustAsync(async (v, ct) => await repoCliente.GetByIdAsync(v, ct) is not null)
            .WithMessage("Cliente: el valor seleccionado no existe");

        RuleFor(x => x.TipoCuentaId).Cascade(CascadeMode.Stop)
            .GreaterThan(0).WithMessage("Seleccione Tipo de cuenta")
            .MustAsync(async (v, ct) => await repoTipoCuenta.GetByIdAsync(v, ct) is not null)
            .WithMessage("Tipo de cuenta: el valor seleccionado no existe");
    }
}