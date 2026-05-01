using FluentValidation;
using ShopComponents.Core.DTOs;

namespace ShopComponents.Services.Validators;

public class FacturaDtoValidator : AbstractValidator<FacturaDto>
{
    public FacturaDtoValidator()
    {
        RuleFor(x => x.VentaId)
            .GreaterThan(0).WithMessage("El ID de venta es obligatorio.");

        RuleFor(x => x.NroFactura)
            .NotEmpty().WithMessage("El número de factura es obligatorio.")
            .MaximumLength(50).WithMessage("El número de factura no puede exceder 50 caracteres.");

        RuleFor(x => x.Fecha)
            .NotNull().WithMessage("La fecha es obligatoria.");

        RuleFor(x => x.Total)
            .GreaterThan(0).WithMessage("El total debe ser mayor que cero.");
    }
}