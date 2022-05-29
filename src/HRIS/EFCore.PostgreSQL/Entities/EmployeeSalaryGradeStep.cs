namespace Nova.HRIS.Entities;

sealed class EmployeeSalaryGradeStepETC : IEntityTypeConfiguration<EmployeeSalaryGradeStep>
{
    public void Configure(EntityTypeBuilder<EmployeeSalaryGradeStep> builder)
    {
        builder.ToTable("EmployeeSalaryGradeStep", "HRIS");

        builder.HasOne(_ => _.Employee)
            .WithMany(_ => _.SalaryGradeSteps)
            .HasForeignKey(_ => _.EmployeeId);
    }
}
