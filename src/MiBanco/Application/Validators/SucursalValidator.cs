using FluentValidation;
using MiBanco.Application.Abstractions;
using MiBanco.Application.Dtos;
using MiBanco.Domain.Entities;

namespace MiBanco.Application.Validators;

public class SucursalValidator : AbstractValidator<SucursalDto>
{
    public SucursalValidator()
    {
        RuleFor(x => x.Nombre).Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Nombre: campo obligatorio")
            .MaximumLength(60).WithMessage("Nombre: máximo 60 caracteres");

        RuleFor(x => x.Ciudad).Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Ciudad: campo obligatorio")
            .MaximumLength(60).WithMessage("Ciudad: máximo 60 caracteres");

        RuleFor(x => x.Direccion).Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Dirección: campo obligatorio")
            .MaximumLength(150).WithMessage("Dirección: máximo 150 caracteres");

        RuleFor(x => x.Telefono).Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Teléfono: campo obligatorio")
            .MaximumLength(15).WithMessage("Teléfono: máximo 15 caracteres")
            .Matches(@"^[0-9+\-\s]{7,15}$").WithMessage("El teléfono debe tener entre 7 y 15 caracteres (dígitos, +, -)");

        RuleFor(x => x.Email).Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Correo: campo obligatorio")
            .MaximumLength(120).WithMessage("Correo: máximo 120 caracteres")
            .Matches(@"^[^@\s]+@[^@\s]+\.[^@\s]+$").WithMessage("El correo no tiene un formato válido")
            .EmailAddress().WithMessage("El correo no tiene un formato válido");
    }
}