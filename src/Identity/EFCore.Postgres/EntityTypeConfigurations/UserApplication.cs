using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Nova.Identity.EntityTypeConfigurations;

sealed class UserApplication_EntityTypeConfiguration : IEntityTypeConfiguration<UserApplication>
{
    public void Configure(EntityTypeBuilder<UserApplication> builder)
    {
        builder.ToTable("UserApplication", DatabaseDefaults.Schema);

        builder.HasOne(userApplication => userApplication.User)
            .WithMany()
            .HasForeignKey(userApplication => userApplication.UserId);

        builder.HasOne(userApplication => userApplication.Application)
            .WithMany()
            .HasForeignKey(userApplication => userApplication.ApplicationId);
    }
}
