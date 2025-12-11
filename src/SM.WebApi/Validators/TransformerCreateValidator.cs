using FluentValidation;
using SM.Shared;

namespace SM.WebApi.Validators;

public class TransformerCreateValidator : AbstractValidator<TransformerDto>
{
    public TransformerCreateValidator()
    {
        RuleFor(x => x.SerialNumber)
            .NotEmpty().WithMessage("Serial number required")
            .MaximumLength(100);

        RuleFor(x => x.ManufacturerName)
            .NotEmpty().WithMessage("Manufacturer required");

        RuleFor(x => x.YearOfManufacture)
            .InclusiveBetween(1900, DateTime.UtcNow.Year);

        RuleFor(x => x.RatedCapacity).GreaterThan(0);
        RuleFor(x => x.PrimaryVoltage).GreaterThan(0);
        RuleFor(x => x.SecondaryVoltage).GreaterThan(0);

        RuleFor(x => x.TransformerType).NotEmpty();
        RuleFor(x => x.VectorGroup).NotEmpty();
    }
}
