using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Nova.Identity.EntityTypeConfigurations;

sealed class UserPermission_EntityTypeConfiguration : IEntityTypeConfiguration<UserPermission>
{
    public void Configure(EntityTypeBuilder<UserPermission> builder)
    {
        builder.ToTable("UserPermission", DatabaseDefaults.Schemas.Identity);

        builder.HasOne(userPermission => userPermission.User)
            .WithMany(user => user.UserPermissions)
            .HasForeignKey(userPermission => userPermission.UserId);

        builder.HasOne(userPermission => userPermission.Permission)
            .WithMany(permission => permission.UserPermissions)
            .HasForeignKey(userPermission => userPermission.PermissionId);
    }
}
