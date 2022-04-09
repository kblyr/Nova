namespace Nova.Identity.Entities;

sealed class UserLoginETC : IEntityTypeConfiguration<UserLogin>
{
    public void Configure(EntityTypeBuilder<UserLogin> builder)
    {
        builder.ToTable("UserLogin", DatabaseDefaults.Schema);

        builder.HasOne(_ => _.User)
            .WithMany()
            .HasForeignKey(_ => _.UserId);

        builder.HasOne(_ => _.Type)
            .WithMany()
            .HasForeignKey(_ => _.TypeId);
    }
}
