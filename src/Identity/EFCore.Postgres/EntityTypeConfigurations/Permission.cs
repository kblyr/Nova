using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Nova.Identity.EntityTypeConfigurations;

sealed class Permission_EntityTypeConfiguration : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.ToTable("Permission", DatabaseDefaults.Schema);

        builder.HasOne(permission => permission.Domain)
            .WithMany()
            .HasForeignKey(permission => permission.DomainId);

        builder.HasOne(permission => permission.Application)
            .WithMany()
            .HasForeignKey(permission => permission.ApplicationId);
    }
}
