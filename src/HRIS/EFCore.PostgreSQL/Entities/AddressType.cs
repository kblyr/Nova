namespace Nova.HRIS.Entities;

sealed class AddressTypeETC : IEntityTypeConfiguration<AddressType>
{
    public void Configure(EntityTypeBuilder<AddressType> builder)
    {
        builder.ToTable("AddressType", "HRIS");
    }
}
