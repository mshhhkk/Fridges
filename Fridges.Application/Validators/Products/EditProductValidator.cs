using FluentValidation;
using Fridges.Application.DTOs;

namespace Fridges.Application.Validators.Products;

public class EditProductValidator : AbstractValidator<EditProductDto>
{
    public EditProductValidator()
    {
        RuleFor(x => x.Weight)
            .GreaterThan(0)
            .WithMessage("Weight field is required");
        RuleFor(x => x.FridgeId)
           .NotEmpty()
           .WithMessage("FridgeId field is required")
           .Must(id => Guid.TryParse(id, out _))
           .WithMessage("FridgeId must be a valid GUID");
        RuleFor(x => x.IsFresh)
           .NotEmpty()
           .WithMessage("IsFresh field is required");
    }
}
