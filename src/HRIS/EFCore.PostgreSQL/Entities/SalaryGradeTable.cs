namespace Nova.HRIS.Entities;

sealed class SalaryGradeTableETC : IEntityTypeConfiguration<SalaryGradeTable>
{
    public void Configure(EntityTypeBuilder<SalaryGradeTable> builder)
    {
        builder.ToTable("SalaryGradeTable", "HRIS");
    }
}
