using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Nova.HRIS.EntityTypeConfigurations;

sealed class SalaryGradeStep_EntityTypeConfiguration : IEntityTypeConfiguration<SalaryGradeStep>
{
    public void Configure(EntityTypeBuilder<SalaryGradeStep> builder)
    {
        builder.ToTable("SalaryGradeStep", DatabaseDefaults.Schemas.HRIS);

        builder.HasOne(salaryGradeStep => salaryGradeStep.Table)
        .WithMany(table => table.Steps)
        .HasForeignKey(salaryGradeStep => salaryGradeStep.TableId);
    }
}
