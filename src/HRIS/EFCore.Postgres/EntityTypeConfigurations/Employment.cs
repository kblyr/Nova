using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Nova.HRIS.EntityTypeConfigurations;

sealed class Employment_EntityTypeConfiguration : IEntityTypeConfiguration<Employment>
{
    public void Configure(EntityTypeBuilder<Employment> builder)
    {
        builder.ToTable("Employment", DatabaseDefaults.Schemas.HRIS);

        builder.HasOne(employment => employment.Employee)
            .WithMany(employee => employee.Employments)
            .HasForeignKey(employment => employment.EmployeeId);

        builder.HasOne(employment => employment.Type)
            .WithMany()
            .HasForeignKey(employment => employment.TypeId);

        builder.HasOne(employment => employment.Office)
            .WithMany()
            .HasForeignKey(employment => employment.OfficeId);

        builder.HasOne(employment => employment.Position)
            .WithMany()
            .HasForeignKey(employment => employment.PositionId);
    }
}
