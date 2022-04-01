using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Nova.Identity.EntityTypeConfigurations;

sealed class User_EntityTypeConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("User", DatabaseDefaults.Schemas.Identity);

        builder.HasOne(user => user.Status)
            .WithMany(status => status.Users)
            .HasForeignKey(user => user.StatusId);
    }
}
