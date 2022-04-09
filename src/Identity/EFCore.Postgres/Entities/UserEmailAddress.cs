namespace Nova.Identity.Entities;

sealed class UserEmailAddressETC : IEntityTypeConfiguration<UserEmailAddress>
{
    public void Configure(EntityTypeBuilder<UserEmailAddress> builder)
    {
        builder.ToTable("UserEmailAddress", DatabaseDefaults.Schema);

        builder.HasOne(_ => _.User)
            .WithMany(_ => _.UserEmailAddresses)
            .HasForeignKey(_ => _.UserId);
    }
}
