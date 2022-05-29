namespace Nova.HRIS.Entities;

sealed class SalaryGradeStepETC : IEntityTypeConfiguration<SalaryGradeStep>
{
    public void Configure(EntityTypeBuilder<SalaryGradeStep> builder)
    {
        builder.ToTable("SalaryGradeStep", "HRIS");

        builder.HasOne(_ => _.Table)
            .WithMany(_ => _.Steps)
            .HasForeignKey(_ => _.TableId);
    }
}
