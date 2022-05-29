namespace Nova.HRIS.Entities;

sealed class CivilStatusETC : IEntityTypeConfiguration<CivilStatus>
{
    public void Configure(EntityTypeBuilder<CivilStatus> builder)
    {
        builder.ToTable("CivilStatus", "HRIS");
    }
}
