namespace SM.Shared;

public class AssetDto
{
    public Guid Id { get; set; }
    public Guid AssetTypeId { get; set; }
    public DateTime InstallationDate { get; set; } = DateTime.UtcNow;
    public Guid SubstationId { get; set; }
    public SubstationDto? Substation { get; set; }
    public string Status { get; set; } = "Active";

    // Navigation
    public TransformerDto? Transformer { get; set; }
}