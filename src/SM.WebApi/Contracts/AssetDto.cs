namespace SM.WebApi.Contracts;

public class AssetDto
{
    public Guid Id { get; set; }
    public Guid AssetTypeId { get; set; }
    public DateTime InstallationDate { get; set; }
    public Guid SubstationId { get; set; }
    public string Status { get; set; } = "Active";
}

public class AssetCreateDto
{
   public Guid AssetTypeId { get; set; }
    public DateTime InstallationDate { get; set; } = DateTime.UtcNow;
    public Guid SubstationId { get; set; }
    public string Status { get; set; } = "Active";
}
