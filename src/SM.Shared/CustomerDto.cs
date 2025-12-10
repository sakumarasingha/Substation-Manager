namespace SM.Shared;

public sealed class CustomerDto
{
    public Guid Id { get; set; }
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? Alias { get; set; }
    public string TenantId { get; set; } = default!;
    public CustomerStatus Status { get; set; }

    public string? PrimaryEmail { get; set; }
    public string? PrimaryPhone { get; set; }
    public string? Website { get; set; }

    public AddressDto? BillingAddress { get; set; }
    public AddressDto? ServiceAddress { get; set; }

    public List<ContactDto> Contacts { get; set; } = new();

    public string? SlaTier { get; set; }
    public string? Notes { get; set; }

    public int AssetCount { get; set; }
    public int ActiveWorkOrders { get; set; }

    public DateTimeOffset CreatedAt { get; set; }
    public string CreatedBy { get; set; } = default!;
    public DateTimeOffset? UpdatedAt { get; set; }
    public string? UpdatedBy { get; set; }
}

public sealed class AddressDto
{
    public string? Line1 { get; set; }
    public string? Line2 { get; set; }
    public string? City { get; set; }
    public string? StateOrProvince { get; set; }
    public string? PostalCode { get; set; }
    public string? Country { get; set; }
}

public sealed class ContactDto
{
    public string Name { get; set; } = default!;
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public string? Role { get; set; }
    public ContactChannel PreferredChannel { get; set; }
}

public enum CustomerStatus
{
    Active = 1,
    Suspended = 2,
    Prospective = 3,
    Archived = 4
}

public enum ContactChannel
{
    Email = 1,
    Phone = 2,
    SMS = 3
}
