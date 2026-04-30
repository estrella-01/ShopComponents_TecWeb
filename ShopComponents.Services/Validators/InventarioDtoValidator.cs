using FluentValidation;
using ShopComponents.Core.DTOs;

namespace ShopComponents.Services.Validators;

public class InventarioDtoValidator : AbstractValidator<InventarioDto>
{
    public InventarioDtoValidator()
    {
        RuleFor(x => x.ProductoId)
            .GreaterThan(0).WithMessage("El producto es obligatorio");

        RuleFor(x => x.Cantidad)
            .GreaterThan(0).WithMessage("La cantidad debe ser mayor que cero");

        RuleFor(x => x.TipoMovimiento)
            .NotEmpty().WithMessage("El tipo de movimiento es obligatorio");
    }
}