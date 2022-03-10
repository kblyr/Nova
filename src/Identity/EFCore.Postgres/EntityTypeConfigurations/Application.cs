using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Nova.Identity.EntityTypeConfigurations;

sealed class Application_EntityTypeConfiguration : IEntityTypeConfiguration<Application>
{
    public void Configure(EntityTypeBuilder<Application> builder)
    {
        builder.ToTable("Application", DatabaseDefaults.Schema);

        builder.HasOne(application => application.Domain)
            .WithMany()
            .HasForeignKey(application => application.DomainId);
    }
}
