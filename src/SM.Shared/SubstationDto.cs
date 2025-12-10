namespace SM.Shared;

public sealed class SubstationDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public Guid CustomerId { get; set; }
    public CustomerDto? Customer { get; set; }

    public List<Transformer> Transformers { get; set; } = new();
}