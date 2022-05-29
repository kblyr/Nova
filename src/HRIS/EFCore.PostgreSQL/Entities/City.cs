namespace Nova.HRIS.Entities;

sealed class CityETC : IEntityTypeConfiguration<City>
{
    public void Configure(EntityTypeBuilder<City> builder)
    {
        builder.ToTable("City", "HRIS");

        builder.HasOne(_ => _.Province)
            .WithMany(_ => _.Cities)
            .HasForeignKey(_ => _.ProvinceId);
    }
}
