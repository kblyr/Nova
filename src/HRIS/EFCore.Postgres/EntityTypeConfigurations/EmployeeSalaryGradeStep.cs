using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Nova.HRIS.EntityTypeConfigurations;

sealed class EmployeeSalaryGradeStep_EntityTypeConfiguration : IEntityTypeConfiguration<EmployeeSalaryGradeStep>
{
    public void Configure(EntityTypeBuilder<EmployeeSalaryGradeStep> builder)
    {
        builder.ToTable("EmployeeSalaryGradeStep", DatabaseDefaults.Schemas.HRIS);

        builder.HasOne(employeeSalaryGradeStep => employeeSalaryGradeStep.Employee)
            .WithMany(employee => employee.SalaryGradeSteps)
            .HasForeignKey(employeeSalaryGradeStep => employeeSalaryGradeStep.EmployeeId);
    }
}
