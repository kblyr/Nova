namespace Nova.HRIS.Entities;

sealed class ProvinceETC : IEntityTypeConfiguration<Province>
{
    public void Configure(EntityTypeBuilder<Province> builder)
    {
        builder.ToTable("Province", "HRIS");
    }
}
