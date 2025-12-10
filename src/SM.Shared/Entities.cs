namespace SM.Shared;

public class Asset
{
    public int Id { get; set; }
    public int AssetTypeId { get; set; }
    public required DateTime InstallationDate { get; set; } = DateTime.UtcNow;
    public int SubstationId { get; set; }
    public SubstationDto? Substation { get; set; }
    public required string Status { get; set; } = "Active";

    // Navigation
    public Transformer? Transformer { get; set; }
}

public class AssetType
{
    public int Id { get; set; }
    public required string Name { get; set; } 
    public string? Description { get; set; }

}


public class Transformer
{
    public int Id { get; set; }   // same as Asset.Id

    // Navigation
    public Asset Asset { get; set; } = null!;
    
    public required string SerialNumber { get; set; }
    public required string ManufacturerName { get; set; }
    public int YearOfManufacture { get; set; }
    public double RatedCapacity { get; set; }
    public double PrimaryVoltage { get; set; }
    public double SecondaryVoltage { get; set; }
    public required string TransformerType { get; set; }
    public required string VectorGroup { get; set; }
}

