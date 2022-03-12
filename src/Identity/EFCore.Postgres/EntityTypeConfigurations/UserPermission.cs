using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Nova.Identity.EntityTypeConfigurations;

sealed class UserPermission_EntityTypeConfiguration : IEntityTypeConfiguration<UserPermission>
{
    public void Configure(EntityTypeBuilder<UserPermission> builder)
    {
        builder.ToTable("UserPermission", DatabaseDefaults.Schema);

        builder.HasOne(userPermission => userPermission.User)
            .WithMany()
            .HasForeignKey(userPermission => userPermission.UserId);

        builder.HasOne(userPermission => userPermission.Permission)
            .WithMany()
            .HasForeignKey(userPermission => userPermission.PermissionId);
    }
}
