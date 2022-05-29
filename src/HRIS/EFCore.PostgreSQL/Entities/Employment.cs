namespace Nova.HRIS.Entities;

sealed class EmploymentETC : IEntityTypeConfiguration<Employment>
{
    public void Configure(EntityTypeBuilder<Employment> builder)
    {
        builder.ToTable("Employment", "HRIS");

        builder.HasOne(_ => _.Employee)
            .WithMany(_ => _.Employments)
            .HasForeignKey(_ => _.EmployeeId);

        builder.HasOne(_ => _.Type)
            .WithMany()
            .HasForeignKey(_ => _.TypeId);

        builder.HasOne(_ => _.Office)
            .WithMany()
            .HasForeignKey(_ => _.OfficeId);

        builder.HasOne(_ => _.Position)
            .WithMany()
            .HasForeignKey(_ => _.PositionId);
    }
}
