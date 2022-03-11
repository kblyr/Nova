using System.Reflection;

namespace Nova.Identity;

sealed class EntityTypeConfigurationContainingAssemblyProvider : IEntityTypeConfigurationContainingAssemblyProvider<DatabaseContext>
{
    public Assembly Assembly { get; } = typeof(EntityTypeConfigurationContainingAssemblyProvider).Assembly;
}