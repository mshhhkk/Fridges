using FluentValidation;
using Fridges.Application.DTOs;
namespace Fridges.Application.Validators.Fridges;

public class AddFridgeValidator : AbstractValidator<FridgeDto>
{
    public AddFridgeValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name field is reuired!")
            .MaximumLength(100)
            .WithMessage("Name must be at most 100 characters long.");

        RuleFor(x => x.Capacity).
            NotEmpty()
            .WithMessage("Capacity field is reuired!")
            .GreaterThanOrEqualTo((short)1)
            .WithMessage("Capacity can not be less than 1");

        RuleFor(x => x.IsFreezer)
            .NotNull()
            .WithMessage("IsFreezer field is required");
    }
}
