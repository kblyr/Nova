using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Nova.HRIS.EntityTypeConfigurations;

sealed class Position_EntityTypeConfiguration : IEntityTypeConfiguration<Position>
{
    public void Configure(EntityTypeBuilder<Position> builder)
    {
        builder.ToTable("Position", DatabaseDefaults.Schemas.HRIS);

        builder.HasOne(position => position.Parent)
            .WithMany(parent => parent.Children)
            .HasForeignKey(position => position.ParentId);
    }
}
