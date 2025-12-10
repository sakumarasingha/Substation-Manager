namespace SM.WebApi.Domain;

public class TransformerAuditReport
{
    public Guid Id { get; set; }
    public required string ReportNumber { get; set; }
    public Guid TransformerId { get; set; }
    public Transformer? Transformer { get; set; }
    public double? WindingTemperature { get; set; }
    public double? TransformerOilLevelPercent { get; set; }
    public bool? SilicaGelBreatherOk { get; set; }
    public bool? BuchholzRelayOk { get; set; }
    public double? OilDielectricBreakdownVoltage { get; set; }
    public double? RequiredBdvLevel { get; set; }
    public double? OilMoistureContentPpm { get; set; }
    public DateTime DateServiced { get; set; }
    public string? Notes { get; set; }

    // Audit
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public string CreatedBy { get; set; } = "system";
    public DateTimeOffset? UpdatedAt { get; set; }
    public string? UpdatedBy { get; set; }

    // Soft-delete
    public bool IsDeleted { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }
    public string? DeletedBy { get; set; }

    // Concurrency
    public byte[]? RowVersion { get; set; }
}
