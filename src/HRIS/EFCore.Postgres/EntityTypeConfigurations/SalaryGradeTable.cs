using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Nova.HRIS.EntityTypeConfigurations;

sealed class SalaryGradeTable_EntityTypeConfiguration : IEntityTypeConfiguration<SalaryGradeTable>
{
    public void Configure(EntityTypeBuilder<SalaryGradeTable> builder)
    {
        builder.ToTable("SalaryGradeTable", DatabaseDefaults.Schemas.HRIS);
    }
}
