namespace SM.WebApi.Contracts;

public class AssetTypeDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = "Transformers";
}

public class AssetTypeCreateDto
{
    public string Name { get; set; } = "Transformers";
}
