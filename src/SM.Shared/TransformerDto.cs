namespace SM.Shared;

public class TransformerDto
{
    public Guid Id { get; set; }   // same as Asset.Id

    public string Name { get; set; } = string.Empty;
    // Navigation
    public AssetDto Asset { get; set; } = null!;

    public string SerialNumber { get; set; } = string.Empty;
    public string ManufacturerName { get; set; } = string.Empty;
    public int YearOfManufacture { get; set; }
    public double? RatedCapacity { get; set; }
    public double? PrimaryVoltage { get; set; }
    public double? SecondaryVoltage { get; set; }
    public string? TransformerType { get; set; } = string.Empty;
    public string? VectorGroup { get; set; } = string.Empty;
}

