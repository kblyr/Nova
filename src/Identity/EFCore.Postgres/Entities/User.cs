namespace Nova.Identity.Entities;

sealed class UserETC : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("User", DatabaseDefaults.Schema);

        builder.HasOne(_ => _.Status)
            .WithMany()
            .HasForeignKey(_ => _.StatusId);
    }
}
