using System.Reflection;

namespace Nova.HRIS;

sealed class EntityTypeConfigurationContainingAssemblyProvider : IEntityTypeConfigurationContainingAssemblyProvider<EmployeeDbContext>
{
    public Assembly Assembly { get; } = typeof(EntityTypeConfigurationContainingAssemblyProvider).Assembly;
}