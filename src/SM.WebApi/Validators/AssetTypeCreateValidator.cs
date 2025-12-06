using FluentValidation;
using SM.WebApi.Contracts;

namespace SM.WebApi.Validators;

public class AssetTypeCreateValidator : AbstractValidator<AssetTypeCreateDto>
{
    public AssetTypeCreateValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Category name is required")
            .MaximumLength(100);
    }
}
