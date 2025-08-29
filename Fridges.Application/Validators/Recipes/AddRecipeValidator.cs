using FluentValidation;
using Fridges.Application.DTOs;
namespace Fridges.Application.Validators.Recipes;

public class AddRecipeValidator : AbstractValidator<RecipeDto>
{
    public AddRecipeValidator()
    {
        RuleFor(x => x.title)
            .NotEmpty()
            .WithMessage("Title field is required")
            .MaximumLength(20)
            .WithMessage("Title must not exceed 256 characters");

        RuleFor(x => x.Instructions)
            .NotEmpty()
            .WithMessage("Instructions field is required")
            .MaximumLength(1000)
            .WithMessage("Instructions must not exceed more than 1000 characters");

        RuleFor(x => x.Products)
            .NotEmpty()
            .WithMessage("At least one product is required");

        RuleForEach(x => x.Products)
          .SetValidator(new RecipeProductValidator());
    }
}
