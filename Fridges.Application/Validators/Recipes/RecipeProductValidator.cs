using FluentValidation;
using Fridges.Application.DTOs;

namespace Fridges.Application.Validators.Recipes;

public class RecipeProductValidator : AbstractValidator<RecipeProductDto>
{
    public RecipeProductValidator()
    {
        RuleFor(x => x.ProductTypeId)
            .GreaterThan(0)
            .WithMessage("ProductTypeId must be a positive number");

        RuleFor(x => x.Quantity)
            .GreaterThan(0)
            .WithMessage("Quantity must be greater than 0")
            .LessThan(1000)
            .WithMessage("Quantity cannot be greater than 1000");

        RuleFor(x => x.UnitType)
            .IsInEnum().WithMessage("UnitType must be a valid enum value");
    }
}
