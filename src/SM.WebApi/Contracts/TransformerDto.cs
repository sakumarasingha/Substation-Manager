using SM.WebApi.Domain;

namespace SM.WebApi.Contracts;

public class TransformerDto
{
    public int Id { get; set; }              // AssetId
    public int SubstationId { get; set; }
    public DateTime InstallationDate { get; set; }
    public required string Status { get; set; }

    public string SerialNumber { get; set; } = string.Empty;
    public string ManufacturerName { get; set; } = string.Empty;
    public int YearOfManufacture { get; set; }
    public double RatedCapacity { get; set; }
    public double PrimaryVoltage { get; set; }
    public double SecondaryVoltage { get; set; }
    public string TransformerType { get; set; } = string.Empty;
    public string VectorGroup { get; set; } = string.Empty;
}

public class TransformerCreateDto
{
    public int SubstationId { get; set; }
    public int AssetTypeId { get; set; }
    public DateTime InstallationDate { get; set; } = DateTime.UtcNow;
    public string Status { get; set; } = "Active";


    public string SerialNumber { get; set; } = string.Empty;
    public string ManufacturerName { get; set; } = string.Empty;
    public int YearOfManufacture { get; set; }
    public double RatedCapacity { get; set; }
    public double PrimaryVoltage { get; set; }
    public double SecondaryVoltage { get; set; }
    public string TransformerType { get; set; } = string.Empty;
    public string VectorGroup { get; set; } = string.Empty;
}
