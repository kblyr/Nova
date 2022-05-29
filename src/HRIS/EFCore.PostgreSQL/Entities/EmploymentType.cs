namespace Nova.HRIS.Entities;

sealed class EmploymentTypeETC : IEntityTypeConfiguration<EmploymentType>
{
    public void Configure(EntityTypeBuilder<EmploymentType> builder)
    {
        builder.ToTable("EmploymentType", "HRIS");
    }
}
