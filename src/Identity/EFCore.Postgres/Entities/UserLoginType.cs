namespace Nova.Identity.Entities;

sealed class UserLoginTypeETC : IEntityTypeConfiguration<UserLoginType>
{
    public void Configure(EntityTypeBuilder<UserLoginType> builder)
    {
        builder.ToTable("UserLoginType", DatabaseDefaults.Schema);
    }
}
