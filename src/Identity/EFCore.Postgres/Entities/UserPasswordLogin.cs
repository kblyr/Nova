namespace Nova.Identity.Entities;

sealed class UserPasswordLoginETC : IEntityTypeConfiguration<UserPasswordLogin>
{
    public void Configure(EntityTypeBuilder<UserPasswordLogin> builder)
    {
        builder.ToTable("UserPasswordLogin", DatabaseDefaults.Schema);
    }
}
