using FluentValidation;
using SM.WebApi.Contracts;

namespace SM.WebApi.Validators;

public class SubstationCreateValidator : AbstractValidator<SubstationCreateDto>
{
    public SubstationCreateValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Substation name is required")
            .MaximumLength(200);

        RuleFor(x => x.CustomerId)
            .GreaterThan(0).WithMessage("CustomerId must be valid");
    }
}
