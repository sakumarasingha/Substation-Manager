namespace SM.WebApi.Contracts;

public class AssetTypeDto
{
    public int Id { get; set; }
    public string Name { get; set; } = "Transformers";
}

public class AssetTypeCreateDto
{
    public string Name { get; set; } = "Transformers";
}
