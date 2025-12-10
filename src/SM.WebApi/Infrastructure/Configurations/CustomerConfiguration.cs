using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SM.WebApi.Domain;

namespace SM.WebApi.Infrastructure.Configurations;

 public sealed class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> b)
        {
            // Table
            b.ToTable("Customers");

            // Keys
            b.HasKey(x => x.Id);

            // Indexes
            b.HasIndex(x => new { x.TenantId, x.Code })
             .IsUnique();

            b.HasIndex(x => new { x.TenantId, x.Status });

            // Properties
            b.Property(x => x.Code)
             .IsRequired()
             .HasMaxLength(32);

            b.Property(x => x.Name)
             .IsRequired()
             .HasMaxLength(200);

            b.Property(x => x.Alias)
             .HasMaxLength(64);

            b.Property(x => x.TenantId)
             .IsRequired()
             .HasMaxLength(64);

            b.Property(x => x.PrimaryEmail)
             .HasMaxLength(200);

            b.Property(x => x.PrimaryPhone)
             .HasMaxLength(32);

            b.Property(x => x.Website)
             .HasMaxLength(256);

            b.Property(x => x.SlaTier)
             .HasMaxLength(64);

            b.Property(x => x.Notes)
             .HasMaxLength(4000);

            b.Property(x => x.CreatedAt)
             .IsRequired();

            b.Property(x => x.CreatedBy)
             .IsRequired()
             .HasMaxLength(100);

            b.Property(x => x.UpdatedBy)
             .HasMaxLength(100);

            b.Property(x => x.DeletedBy)
             .HasMaxLength(100);

            // Concurrency token
            b.Property(x => x.RowVersion)
             .IsRowVersion()
             .IsConcurrencyToken();

            // Global query filters (soft-delete)
            b.HasQueryFilter(x => !x.IsDeleted);

            // Owned types: Addresses
            b.OwnsOne(x => x.BillingAddress, a =>
            {
                a.Property(p => p.Line1).HasMaxLength(128);
                a.Property(p => p.Line2).HasMaxLength(128);
                a.Property(p => p.City).HasMaxLength(80);
                a.Property(p => p.StateOrProvince).HasMaxLength(80);
                a.Property(p => p.PostalCode).HasMaxLength(32);
                a.Property(p => p.Country).HasMaxLength(80);

                // Uncomment if provider supports JSON columns (EF Core 8+)
                // a.ToJson();
            });
        }
    }
    