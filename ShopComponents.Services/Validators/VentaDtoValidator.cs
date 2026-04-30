using FluentValidation;
using ShopComponents.Core.DTOs;

namespace ShopComponents.Services.Validators;

public class VentaDtoValidator : AbstractValidator<VentaDto>
{
    public VentaDtoValidator()
    {
        RuleFor(x => x.ClienteId)
            .NotNull().WithMessage("El cliente es obligatorio.")
            .GreaterThan(0).WithMessage("El cliente debe ser mayor que cero.");

        RuleFor(x => x.UsuarioId)
            .NotNull().WithMessage("El usuario es obligatorio.")
            .GreaterThan(0).WithMessage("El usuario debe ser mayor que cero.");

        RuleFor(x => x.Fecha)
            .NotNull().WithMessage("La fecha es obligatoria.");

        RuleFor(x => x.Total)
            .GreaterThan(0).WithMessage("El total debe ser mayor que cero.");
    }
}