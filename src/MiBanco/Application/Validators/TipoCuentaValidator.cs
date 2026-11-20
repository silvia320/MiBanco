using FluentValidation;
using MiBanco.Application.Abstractions;
using MiBanco.Application.Dtos;
using MiBanco.Domain.Entities;

namespace MiBanco.Application.Validators;

public class TipoCuentaValidator : AbstractValidator<TipoCuentaDto>
{
    public TipoCuentaValidator(IRepository<TipoCuenta> self)
    {
        RuleFor(x => x.Nombre).Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Nombre: campo obligatorio")
            .MaximumLength(60).WithMessage("Nombre: máximo 60 caracteres")
            .MustAsync(async (dto, v, ct) => !await self.AnyAsync(e => e.Nombre == v.Trim() && e.Id != dto.Id, ct))
            .WithMessage("Nombre: ya existe un registro con ese valor");

        RuleFor(x => x.Descripcion).Cascade(CascadeMode.Stop)
            .MaximumLength(150).WithMessage("Descripción: máximo 150 caracteres");

        RuleFor(x => x.TasaInteres).Cascade(CascadeMode.Stop)
            .GreaterThanOrEqualTo(0m).WithMessage("Tasa de interés (%) no puede ser negativo")
            .LessThanOrEqualTo(100m).WithMessage("Tasa de interés (%) no puede superar 100");

        RuleFor(x => x.CostoMantenimiento).Cascade(CascadeMode.Stop)
            .GreaterThanOrEqualTo(0m).WithMessage("Costo de mantenimiento ($) no puede ser negativo")
            .LessThanOrEqualTo(1000m).WithMessage("Costo de mantenimiento ($) no puede superar 1000");
    }
}