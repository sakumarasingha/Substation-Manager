
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SM.WebApi.Domain;

namespace SM.WebApi.Infrastructure.Configurations;

public class SubstationEntityTypeConfiguration : IEntityTypeConfiguration<Substation>
{
       public void Configure(EntityTypeBuilder<Substation> builder)
       {
              builder.HasKey(s => s.Id);

              builder.HasIndex(s => s.Code).IsUnique();

              builder.OwnsOne(s => s.Address);
              builder.OwnsOne(s => s.Coordinates);

              builder.Property(s => s.CustomerId)
                             .IsRequired();

              builder.HasOne(s => s.Customer)
                     .WithMany()
                     .HasForeignKey(s => s.CustomerId)
                     .OnDelete(DeleteBehavior.Restrict);


              // ---------- Scalar properties ----------
              // Id
              builder.Property(s => s.Id)
                     .HasColumnName("Id");

              // Code (required, unique, max 128)
              builder.Property(s => s.Code)
                     .HasMaxLength(128)
                     .IsRequired();

              // Name (required, max 256)
              builder.Property(s => s.Name)
                     .HasMaxLength(256)
                     .IsRequired();


              // Status (enum as int by default; switch to string if desired)
              builder.Property(s => s.Status)
                     .HasConversion<int>()   // store as int; use .HasConversion<string>() if you prefer text
                     .IsRequired();

              // Dates
              builder.Property(s => s.CommissioningDate)
                     .HasColumnType("date")
                     .IsRequired(false);

              builder.Property(s => s.DecommissioningDate)
                     .HasColumnType("date")
                     .IsRequired(false);


              // Audit
              builder.Property(s => s.CreatedAt)
                     .IsRequired();

              builder.Property(s => s.UpdatedAt)
                     .IsRequired(false);

              builder.Property(s => s.UpdatedBy)
                     .IsRequired(false);


              // Coordinates
              builder.OwnsOne(s => s.Coordinates, geo =>
              {
                     geo.Property(g => g.Latitude)
                  .HasPrecision(9, 6) // ~0.1 meter precision
                  .IsRequired(false);

                     geo.Property(g => g.Longitude)
                  .HasPrecision(9, 6)
                  .IsRequired(false);

                     geo.Property(g => g.ElevationMeters)
                  .HasPrecision(8, 2)
                  .IsRequired(false);
              });

       }
}
