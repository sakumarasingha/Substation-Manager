using FluentValidation;
using SM.Shared;

namespace SM.WebApi.Validators;

public class SubstationCreateValidator : AbstractValidator<SubstationDto>
{
    public SubstationCreateValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Substation name is required")
            .MaximumLength(256);

        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("Substation code is required")
            .MaximumLength(128);
    }
}
