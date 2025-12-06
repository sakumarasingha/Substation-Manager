using System.ComponentModel.DataAnnotations;

namespace SM.WebApi.Domain;

public class AssetType
{
    public int Id { get; set; }
    public required string Name { get; set; } 

    [MaxLength(500)] 
    public string? Description { get; set; }

}
