using System.ComponentModel.DataAnnotations;

namespace SM.WebApi.Domain;

public sealed class Substation
{
    // Identity
    public Guid Id { get; set; } = Guid.NewGuid();
    public required string Code { get; set; }
    public required string Name { get; set; }
    public Guid CustomerId { get; set; }

    // Location
    public Address? Address { get; set; } = new Address();
    public GeoCoordinate? Coordinates { get; set; } = new GeoCoordinate();


    // Lifecycle & condition
    public DateTime? CommissioningDate { get; set; }
    public DateTime? DecommissioningDate { get; set; }
    public SubstationStatus Status { get; set; } = SubstationStatus.InService;


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

    public Customer? Customer { get; set; }
    public List<Transformer> Transformers { get; set; } = new();



    // Convenience
    public void SetInService(DateTime date)
    {
        Status = SubstationStatus.InService;
        CommissioningDate = date;
        UpdatedAt = DateTime.UtcNow;
    }

    public void SetOutOfService()
    {
        Status = SubstationStatus.OutOfService;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Decommission(DateTime date)
    {
        Status = SubstationStatus.Decommissioned;
        DecommissioningDate = date;
        UpdatedAt = DateTime.UtcNow;
    }

}


public sealed class GeoCoordinate
{
    [Range(-90, 90)]
    public double? Latitude { get; set; }

    [Range(-180, 180)]
    public double? Longitude { get; set; }

    // Optional: elevation in meters
    public double? ElevationMeters { get; set; }
}


public enum SubstationStatus
{
    Planned = 0,
    InService = 1,
    OutOfService = 2,
    Decommissioned = 3
}

