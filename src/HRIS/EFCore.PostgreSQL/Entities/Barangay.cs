namespace Nova.HRIS.Entities;

sealed class BarangayETC : IEntityTypeConfiguration<Barangay>
{
    public void Configure(EntityTypeBuilder<Barangay> builder)
    {
        builder.ToTable("Barangay", "HRIS");

        builder.HasOne(_ => _.City)
            .WithMany(_ => _.Barangays)
            .HasForeignKey(_ => _.CityId);
    }
}
