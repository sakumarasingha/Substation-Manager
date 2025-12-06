using System.ComponentModel.DataAnnotations;

namespace SM.WebApi.Domain;

public class Asset
{
    public int Id { get; set; }
    public int AssetTypeId { get; set; }
    public required DateTime InstallationDate { get; set; } = DateTime.UtcNow;
    public int SubstationId { get; set; }
    public Substation? Substation { get; set; }
    public required string Status { get; set; } = "Active";

    // Navigation
    public Transformer? Transformer { get; set; }
}