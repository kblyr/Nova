using System.Reflection;

namespace Nova.HRIS;

sealed class EntityTypeConfigurationContainingAssemblyProvider : 
    IEntityTypeConfigurationContainingAssemblyProvider<BarangayDbContext>,
    IEntityTypeConfigurationContainingAssemblyProvider<CityDbContext>,
    IEntityTypeConfigurationContainingAssemblyProvider<EmployeeDbContext>,
    IEntityTypeConfigurationContainingAssemblyProvider<ProvinceDbContext>
{
    public Assembly Assembly { get; } = typeof(EntityTypeConfigurationContainingAssemblyProvider).Assembly;
}