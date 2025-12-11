
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SM.WebApi.Domain;

namespace SM.WebApi.Infrastructure.Configurations;

public class TransformerEntityTypeConfiguration : IEntityTypeConfiguration<Transformer>
{
       public void Configure(EntityTypeBuilder<Transformer> builder)
       {
              builder.HasKey(s => s.Id);

              builder
                  .HasOne(t => t.Asset)
                  .WithOne() // if Asset has no navigation to Transformer; otherwise use .WithOne(a => a.Transformer)
                  .HasForeignKey<Transformer>(t => t.Id)
                  .IsRequired() // the FK must exist; Transformer cannot exist without Asset
                  .OnDelete(DeleteBehavior.Cascade);


              // ---------- Scalar properties ----------
              // Id
              builder.Property(s => s.Id)
                     .HasColumnName("Id");


              builder.Property(t => t.SerialNumber)
                     .IsRequired()
                     .HasMaxLength(100);


       }
}
