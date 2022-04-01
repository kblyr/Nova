using System.Reflection;

namespace Nova.Identity;

sealed class EntityTypeConfigurationContainingAssemblyProvider : 
    IEntityTypeConfigurationContainingAssemblyProvider<AccessTokenDbContext>,
    IEntityTypeConfigurationContainingAssemblyProvider<PermissionDbContext>,
    IEntityTypeConfigurationContainingAssemblyProvider<RoleDbContext>,
    IEntityTypeConfigurationContainingAssemblyProvider<UserDbContext>,
    IEntityTypeConfigurationContainingAssemblyProvider<UserApplicationDbContext>,
    IEntityTypeConfigurationContainingAssemblyProvider<UserStatusDbContext>
{
    public Assembly Assembly { get; } = typeof(EntityTypeConfigurationContainingAssemblyProvider).Assembly;
}