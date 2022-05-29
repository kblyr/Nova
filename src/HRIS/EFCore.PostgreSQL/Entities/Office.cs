namespace Nova.HRIS.Entities;

sealed class OfficeETC : IEntityTypeConfiguration<Office>
{
    public void Configure(EntityTypeBuilder<Office> builder)
    {
        builder.ToTable("Office", "HRIS");

        builder.HasOne(_ => _.Head)
            .WithMany()
            .HasForeignKey(_ => _.HeadId);
    }
}
