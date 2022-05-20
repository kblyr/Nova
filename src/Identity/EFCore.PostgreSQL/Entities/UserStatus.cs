namespace Nova.Identity.Entities;

sealed class UserStatusETC : IEntityTypeConfiguration<UserStatus>
{
    public void Configure(EntityTypeBuilder<UserStatus> builder)
    {
        builder.ToTable("UserStatus", "Identity");
    }
}
