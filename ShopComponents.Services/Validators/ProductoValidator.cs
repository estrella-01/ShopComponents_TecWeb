using FluentValidation;
using ShopComponents.Core.DTOs;

namespace ShopComponents.Services.Validators;

public class ProductoValidator : AbstractValidator<ProductoDto>
{
    public ProductoValidator()
    {
        RuleFor(x => x.Nombre)
            .NotEmpty().WithMessage("El nombre es obligatorio")
            .MinimumLength(3).WithMessage("Debe tener al menos 3 caracteres");

        RuleFor(x => x.Precio)
            .GreaterThan(0).WithMessage("El precio debe ser mayor a 0");

        RuleFor(x => x.Stock)
            .GreaterThanOrEqualTo(0).WithMessage("El stock no puede ser negativo");

        RuleFor(x => x.CategoriaId)
            .NotNull().WithMessage("La categoría es obligatoria");
    }
}