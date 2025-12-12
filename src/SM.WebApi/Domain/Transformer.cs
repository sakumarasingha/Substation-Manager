using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SM.WebApi.Domain;

public class Transformer
{
    [Key, ForeignKey("Asset")]
    public Guid Id { get; set; }   // same as Asset.Id
   

    public required string Name { get; set; }

    // Navigation
    public Asset? Asset { get; set; }

    public required string SerialNumber { get; set; }
    public required string ManufacturerName { get; set; }
    public int YearOfManufacture { get; set; }
    public double? RatedCapacity { get; set; }
    public double? PrimaryVoltage { get; set; }
    public double? SecondaryVoltage { get; set; }
    public string? TransformerType { get; set; }
    public string? VectorGroup { get; set; }

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
    public List<TransformerAuditReport> AuditReports { get; set; } = new();
}