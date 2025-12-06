using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SM.WebApi.Domain;

public class Transformer
{
    [Key, ForeignKey("Asset")]
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
    public List<TransformerAuditReport> AuditReports { get; set; } = new();
}