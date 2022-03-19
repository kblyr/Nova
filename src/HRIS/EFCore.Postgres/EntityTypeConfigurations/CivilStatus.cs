using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Nova.HRIS.EntityTypeConfigurations;

sealed class CivilStatus_EntityTypeConfiguration : IEntityTypeConfiguration<CivilStatus>
{
    public void Configure(EntityTypeBuilder<CivilStatus> builder)
    {
        builder.ToTable("CivilStatus", DatabaseDefaults.Schemas.Lookup);
    }
}
