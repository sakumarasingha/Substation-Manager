namespace SM.WebApi.Contracts;

public class AuditReportDto
{
    public int Id { get; set; }
    public string ReportNumber { get; set; } = string.Empty;
    public int TransformerId { get; set; }
    public DateTime DateServiced { get; set; }

    public double? WindingTemperature { get; set; }
    public double? TransformerOilLevelPercent { get; set; }
    public bool? SilicaGelBreatherOk { get; set; }
    public bool? BuchholzRelayOk { get; set; }
    public double? OilDielectricBreakdownVoltage { get; set; }
    public double? RequiredBdvLevel { get; set; }
    public double? OilMoistureContentPpm { get; set; }
}

public class AuditReportCreateDto
{
    public string ReportNumber { get; set; } = string.Empty;
    public int TransformerId { get; set; }
    public DateTime DateServiced { get; set; }

    public double? WindingTemperature { get; set; }
    public double? TransformerOilLevelPercent { get; set; }
    public bool? SilicaGelBreatherOk { get; set; }
    public bool? BuchholzRelayOk { get; set; }
    public double? OilDielectricBreakdownVoltage { get; set; }
    public double? RequiredBdvLevel { get; set; }
    public double? OilMoistureContentPpm { get; set; }
}
