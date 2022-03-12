using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Nova.Identity.EntityTypeConfigurations;

sealed class RolePermission_EntityTypeConfiguration : IEntityTypeConfiguration<RolePermission>
{
    public void Configure(EntityTypeBuilder<RolePermission> builder)
    {
        builder.ToTable("RolePermission", DatabaseDefaults.Schema);

        builder.HasOne(rolePermission => rolePermission.Role)
            .WithMany()
            .HasForeignKey(rolePermission => rolePermission.RoleId);

        builder.HasOne(rolePermission => rolePermission.Permission)
            .WithMany()
            .HasForeignKey(rolePermission => rolePermission.PermissionId);
    }
}
