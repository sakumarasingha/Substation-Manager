using System.ComponentModel.DataAnnotations;

namespace SM.WebApi.Domain;

public class Customer
{
    public int Id { get; set; }
    [Required, MaxLength(200)] public string Name { get; set; } = string.Empty;
    [Required, EmailAddress, MaxLength(200)] public string Email { get; set; } = string.Empty;

    public string Phone { get; set; } = string.Empty;
    public List<Substation> Substations { get; set; } = new();
}
