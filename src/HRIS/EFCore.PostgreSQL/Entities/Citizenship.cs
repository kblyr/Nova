namespace Nova.HRIS.Entities;

sealed class CitizenshipETC : IEntityTypeConfiguration<Citizenship>
{
    public void Configure(EntityTypeBuilder<Citizenship> builder)
    {
        builder.ToTable("Citizenship", "HRIS");
    }
}
