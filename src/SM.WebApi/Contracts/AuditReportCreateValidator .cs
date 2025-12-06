using FluentValidation;
using SM.WebApi.Contracts;

public class AuditReportCreateValidator : AbstractValidator<AuditReportCreateDto>
{
    public AuditReportCreateValidator()
    {
        RuleFor(x => x.ReportNumber)
            .NotEmpty().WithMessage("Report number is required")
            .MaximumLength(100);

        RuleFor(x => x.TransformerId)
            .GreaterThan(0).WithMessage("TransformerId must be valid");

        RuleFor(x => x.DateServiced)
            .LessThanOrEqualTo(DateTime.UtcNow)
            .WithMessage("DateServiced cannot be in the future");

        RuleFor(x => x.WindingTemperature)
            .InclusiveBetween(0, 300)
            .When(x => x.WindingTemperature.HasValue);

        RuleFor(x => x.TransformerOilLevelPercent)
            .InclusiveBetween(0, 100)
            .When(x => x.TransformerOilLevelPercent.HasValue);

        RuleFor(x => x.OilDielectricBreakdownVoltage)
            .GreaterThan(0)
            .When(x => x.OilDielectricBreakdownVoltage.HasValue);

        RuleFor(x => x.RequiredBdvLevel)
            .GreaterThan(0)
            .When(x => x.RequiredBdvLevel.HasValue);

        RuleFor(x => x.OilMoistureContentPpm)
            .InclusiveBetween(0, 1000)
            .When(x => x.OilMoistureContentPpm.HasValue);
    }
}
