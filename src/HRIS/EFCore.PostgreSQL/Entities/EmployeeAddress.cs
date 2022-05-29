namespace Nova.HRIS.Entities;

sealed class EmployeeAddressETC : IEntityTypeConfiguration<EmployeeAddress>
{
    public void Configure(EntityTypeBuilder<EmployeeAddress> builder)
    {
        builder.ToTable("EmployeeAddress", "HRIS");

        builder.HasOne(_ => _.Employee)
            .WithMany(_ => _.Addresses)
            .HasForeignKey(_ => _.EmployeeId);

        builder.HasOne(_ => _.Type)
            .WithMany()
            .HasForeignKey(_ => _.TypeId);

        builder.HasOne(_ => _.Barangay)
            .WithMany()
            .HasForeignKey(_ => _.BarangayId);

        builder.HasOne(_ => _.City)
            .WithMany()
            .HasForeignKey(_ => _.CityId);

        builder.HasOne(_ => _.Province)
            .WithMany()
            .HasForeignKey(_ => _.ProvinceId);
    }
}
