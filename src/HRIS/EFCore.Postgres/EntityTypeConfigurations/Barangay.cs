using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Nova.HRIS.EntityTypeConfigurations;

sealed class Barangay_EntityTypeConfiguration : IEntityTypeConfiguration<Barangay>
{
    public void Configure(EntityTypeBuilder<Barangay> builder)
    {
        builder.ToTable("Barangay", DatabaseDefaults.Schemas.PoliticalArea);

        builder.HasOne(barangay => barangay.City)
            .WithMany()
            .HasForeignKey(barangay => barangay.CityId);
    }
}
