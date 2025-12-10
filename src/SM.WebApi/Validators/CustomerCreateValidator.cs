using FluentValidation;
using SM.Shared;

namespace SM.WebApi.Validators;
public class CustomerCreateValidator : AbstractValidator<CustomerDto>
{
    public CustomerCreateValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Customer name is required")
            .MaximumLength(200);

        RuleFor(x => x.PrimaryEmail)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Invalid email format");
    }
}