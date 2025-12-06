namespace SM.WebApi.Contracts;

public class AssetDto
{
    public int Id { get; set; }
    public int AssetTypeId { get; set; }
    public DateTime InstallationDate { get; set; }
    public int SubstationId { get; set; }
    public string Status { get; set; } = "Active";
}

public class AssetCreateDto
{
   public int AssetTypeId { get; set; }
    public DateTime InstallationDate { get; set; } = DateTime.UtcNow;
    public int SubstationId { get; set; }
    public string Status { get; set; } = "Active";
}
