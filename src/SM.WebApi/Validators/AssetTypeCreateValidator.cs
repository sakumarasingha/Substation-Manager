using FluentValidation;
using SM.Shared;

namespace SM.WebApi.Validators;

public class AssetTypeCreateValidator : AbstractValidator<AssetTypeDto>
{
    public AssetTypeCreateValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Category name is required")
            .MaximumLength(100);
    }
}
