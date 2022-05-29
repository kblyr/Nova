namespace Nova.HRIS.Entities;

sealed class EmployeeETC : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.ToTable("Employee", "HRIS");

        builder.HasOne(_ => _.CivilStatus)
            .WithMany()
            .HasForeignKey(_ => _.CivilStatusId);

        builder.HasOne(_ => _.Citizenship)
            .WithMany()
            .HasForeignKey(_ => _.CitizenshipId);

        builder.HasOne(_ => _.EmploymentStatus)
            .WithMany()
            .HasForeignKey(_ => _.EmploymentStatusId);
    }
}
