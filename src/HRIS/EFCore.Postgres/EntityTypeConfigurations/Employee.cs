using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Nova.HRIS.EntityTypeConfigurations;

sealed class Employee_EntityTypeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.ToTable("Employee", DatabaseDefaults.Schemas.HRIS);

        builder.HasOne(employee => employee.CivilStatus)
            .WithMany()
            .HasForeignKey(employee => employee.CivilStatusId);

        builder.HasOne(employee => employee.Nationality)
            .WithMany()
            .HasForeignKey(employee => employee.NationalityId);

        builder.HasOne(employee => employee.Province)
            .WithMany()
            .HasForeignKey(employee => employee.ProvinceId);

        builder.HasOne(employee => employee.City)
            .WithMany()
            .HasForeignKey(employee => employee.CityId);

        builder.HasOne(employee => employee.Barangay)
            .WithMany()
            .HasForeignKey(employee => employee.BarangayId);
    }
}