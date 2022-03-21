using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Nova.Identity.EntityTypeConfigurations;

sealed class Application_EntityTypeConfiguration : IEntityTypeConfiguration<Application>
{
    public void Configure(EntityTypeBuilder<Application> builder)
    {
        builder.ToTable("Application", DatabaseDefaults.Schemas.Default);

        builder.HasOne(application => application.Domain)
            .WithMany(domain => domain.Applications)
            .HasForeignKey(application => application.DomainId);
    }
}
