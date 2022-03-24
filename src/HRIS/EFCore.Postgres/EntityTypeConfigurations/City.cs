using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Nova.HRIS.EntityTypeConfigurations;

sealed class City_EntityTypeConfiguration : IEntityTypeConfiguration<City>
{
    public void Configure(EntityTypeBuilder<City> builder)
    {
        builder.ToTable("City", DatabaseDefaults.Schemas.HRIS);

        builder.HasOne(city => city.Province)
            .WithMany()
            .HasForeignKey(city => city.ProvinceId);
    }
}
