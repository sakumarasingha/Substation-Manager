using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace SM.WebApi.Domain;


public sealed class Customer
{
    // Identity
    public Guid Id { get; set; } = Guid.NewGuid();

    // Business keys
    public string Code { get; set; } = default!;   // e.g., "CUST-000123"
    public string Name { get; set; } = default!;
    public string? Alias { get; set; }
    public string TenantId { get; set; } = default!;

    // Status
    public CustomerStatus Status { get; set; } = CustomerStatus.Active;

    // Primary contact
    public string? PrimaryEmail { get; set; }
    public string? PrimaryPhone { get; set; }
    public string? Website { get; set; }

    // Addresses (owned)
    public Address? BillingAddress { get; set; }

    // Operational fields
    public string? SlaTier { get; set; }
    public string? Notes { get; set; }

    public int AssetCount { get; set; }
    public int ActiveWorkOrders { get; set; }

    // Audit
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public string CreatedBy { get; set; } = "system";
    public DateTimeOffset? UpdatedAt { get; set; }
    public string? UpdatedBy { get; set; }

    // Soft-delete
    public bool IsDeleted { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }
    public string? DeletedBy { get; set; }

    // Concurrency
    public byte[]? RowVersion { get; set; }
}

public enum CustomerStatus
{
    Active = 1,
    Suspended = 2,
    Prospective = 3,
    Archived = 4
}

public class Address
{
    public string? Line1 { get; set; }
    public string? Line2 { get; set; }
    public string? City { get; set; }
    public string? StateOrProvince { get; set; }
    public string? PostalCode { get; set; }
    public string? Country { get; set; }
}

public class Contact
{
    public string Name { get; set; } = default!;
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public string? Role { get; set; }
    public ContactChannel PreferredChannel { get; set; } = ContactChannel.Email;
}

public enum ContactChannel
{
    Email = 1,
    Phone = 2,
    SMS = 3
}

