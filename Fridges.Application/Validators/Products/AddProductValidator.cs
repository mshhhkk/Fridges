using FluentValidation;
using Fridges.Application.DTOs;

namespace Fridges.Application.Validators.Products;

public class AddProductValidator : AbstractValidator<AddProductDto>
{
    public AddProductValidator()
    {
        RuleFor(x => x.Weight)
            .GreaterThan(0)
            .WithMessage("Weight field is required");

        RuleFor(x => x.Expiration)
            .NotEmpty()
            .WithMessage("Expiration field is required")
            .Must(d => DateOnly.TryParse(d, out _))
            .WithMessage("Expiration must be a valid DateTime: yyyy-MM-dd");

        RuleFor(x => x.Release)
            .NotEmpty().WithMessage("Release field is required")
            .Must(d => DateOnly.TryParse(d, out _))
            .WithMessage("Realese must be a valid DateTime: yyyy-MM-dd");

        RuleFor(x => x.FridgeId)
            .NotEmpty()
            .WithMessage("FridgeId field is required")
            .Must(id => Guid.TryParse(id, out _))
            .WithMessage("FridgeId must be a valid GUID");

        RuleFor(x => x.ProductTypeId)
            .NotEmpty()
            .WithMessage("PeoductTypeId field is required");

        RuleFor(x => x.IsFresh)
            .NotEmpty()
            .WithMessage("IsFresh field is required");
    }
}
