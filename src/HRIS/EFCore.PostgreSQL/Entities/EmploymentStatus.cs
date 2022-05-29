namespace Nova.HRIS.Entities;

sealed class EmploymentStatusETC : IEntityTypeConfiguration<EmploymentStatus>
{
    public void Configure(EntityTypeBuilder<EmploymentStatus> builder)
    {
        builder.ToTable("EmploymentStatus", "HRIS");
    }
}
