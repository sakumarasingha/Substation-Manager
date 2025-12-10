using System.ComponentModel.DataAnnotations;

namespace SM.WebApi.Domain;

public class AssetType
{
    // Identity
    public Guid Id { get; set; } = Guid.NewGuid();
    public required string Name { get; set; }

    [MaxLength(500)]
    public string? Description { get; set; }

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
