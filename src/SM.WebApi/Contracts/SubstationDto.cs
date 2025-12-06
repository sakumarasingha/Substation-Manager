namespace SM.WebApi.Contracts;

public class SubstationDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int CustomerId { get; set; }
}

public class SubstationCreateDto
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int CustomerId { get; set; }
}
