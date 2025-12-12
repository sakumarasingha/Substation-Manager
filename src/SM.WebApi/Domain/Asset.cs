using System.ComponentModel.DataAnnotations;

namespace SM.WebApi.Domain;

public class Asset
{
    // Identity
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid AssetTypeId { get; set; }
    public required DateTime InstallationDate { get; set; } = DateTime.UtcNow;
    public Guid SubstationId { get; set; }

    public Guid CustomerId { get; set; }
    public required string Status { get; set; } = "Active";

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

    // Navigation

    public AssetType AssetType { get; set; } = null!;
    public Substation Substation { get; set; } = null!;

}