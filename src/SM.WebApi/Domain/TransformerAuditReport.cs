namespace SM.WebApi.Domain;

public class TransformerAuditReport
{
    public int Id { get; set; }
    public required string ReportNumber { get; set; }
    public int TransformerId { get; set; }
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
}
