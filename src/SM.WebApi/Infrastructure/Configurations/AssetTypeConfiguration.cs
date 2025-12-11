using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SM.WebApi.Domain;

namespace SM.WebApi.Infrastructure.Configurations;

public sealed class AssetTypeConfiguration : IEntityTypeConfiguration<AssetType>
{
    public void Configure(EntityTypeBuilder<AssetType> b)
    {
        // Table
        b.ToTable("AssetTypes");

        // Keys
        b.HasKey(x => x.Id);


        // Properties
        b.Property(x => x.Code)
         .IsRequired()
         .HasMaxLength(32);

    }
}
