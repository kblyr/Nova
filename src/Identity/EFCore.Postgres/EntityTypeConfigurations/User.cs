using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Nova.Identity.EntityTypeConfigurations;

sealed class User_EntityTypeConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("User", DatabaseDefaults.Schema);

        builder.HasOne(user => user.Status)
            .WithMany()
            .HasForeignKey(user => user.StatusId);
    }
}
