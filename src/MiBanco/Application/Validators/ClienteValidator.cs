using FluentValidation;
using MiBanco.Application.Abstractions;
using MiBanco.Application.Dtos;
using MiBanco.Domain.Entities;

namespace MiBanco.Application.Validators;

public class ClienteValidator : AbstractValidator<ClienteDto>
{
    public ClienteValidator(IRepository<Cliente> self, IRepository<Sucursal> repoSucursal)
    {
        RuleFor(x => x.Cedula).Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Cédula: campo obligatorio")
            .MaximumLength(10).WithMessage("Cédula: máximo 10 caracteres")
            .Matches(@"^\d{10}$").WithMessage("La cédula debe tener exactamente 10 dígitos")
            .MustAsync(async (dto, v, ct) => !await self.AnyAsync(e => e.Cedula == v.Trim() && e.Id != dto.Id, ct))
            .WithMessage("Cédula: ya existe un registro con ese valor");

        RuleFor(x => x.NombreCompleto).Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Nombre completo: campo obligatorio")
            .MaximumLength(120).WithMessage("Nombre completo: máximo 120 caracteres");

        RuleFor(x => x.Email).Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Correo: campo obligatorio")
            .MaximumLength(120).WithMessage("Correo: máximo 120 caracteres")
            .EmailAddress().WithMessage("El correo no tiene un formato válido")
            .MustAsync(async (dto, v, ct) => !await self.AnyAsync(e => e.Email == v.Trim().ToLowerInvariant() && e.Id != dto.Id, ct))
            .WithMessage("Correo: ya existe un registro con ese valor");

        RuleFor(x => x.SucursalId).Cascade(CascadeMode.Stop)
            .GreaterThan(0).WithMessage("Seleccione Sucursal")
            .MustAsync(async (v, ct) => await repoSucursal.GetByIdAsync(v, ct) is not null)
            .WithMessage("Sucursal: el valor seleccionado no existe");
    }
}