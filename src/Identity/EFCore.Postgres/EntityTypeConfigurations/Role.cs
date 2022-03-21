using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Nova.Identity.EntityTypeConfigurations;

sealed class Role_EntityTypeConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("Role", DatabaseDefaults.Schemas.Default);

        builder.HasOne(role => role.Domain)
            .WithMany(domain => domain.Roles)
            .HasForeignKey(role => role.DomainId);

        builder.HasOne(role => role.Application)
            .WithMany(application => application.Roles)
            .HasForeignKey(role => role.ApplicationId);
    }
}
