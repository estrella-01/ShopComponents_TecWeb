using FluentValidation;
using ShopComponents.Core.DTOs;

namespace ShopComponents.Services.Validators;

public class ProformaDtoValidator : AbstractValidator<ProformaDto>
{
    public ProformaDtoValidator()
    {
        RuleFor(x => x.ClienteId)
            .GreaterThan(0).WithMessage("El cliente es obligatorio.");

        RuleFor(x => x.Detalles)
            .NotEmpty().WithMessage("La proforma debe tener al menos un producto.");

        RuleForEach(x => x.Detalles).ChildRules(detalle =>
        {
            detalle.RuleFor(d => d.ProductoId)
                .GreaterThan(0).WithMessage("El producto es obligatorio.");
            detalle.RuleFor(d => d.Cantidad)
                .GreaterThan(0).WithMessage("La cantidad debe ser mayor que cero.");
            detalle.RuleFor(d => d.Precio)
                .GreaterThan(0).WithMessage("El precio debe ser mayor que cero.");
        });
    }
}