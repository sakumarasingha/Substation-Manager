using System.ComponentModel.DataAnnotations;

namespace SM.WebApi.Domain;

public class Substation
{
    public int Id { get; set; }
    [Required, MaxLength(200)] public string Name { get; set; } = string.Empty;
    public int CustomerId { get; set; }
    public Customer? Customer { get; set; }

    [MaxLength(1000)] 
    public string? Description { get; set; }

    [MaxLength(500)] 
    public string? Location { get; set; }
    
    public List<Transformer> Transformers { get; set; } = new();
}