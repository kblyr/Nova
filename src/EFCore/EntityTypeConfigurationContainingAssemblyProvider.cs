using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace Nova;

public interface IEntityTypeConfigurationContainingAssemblyProvider<T> where T : DbContext
{
    Assembly Assembly { get; }
}

sealed class EntityTypeConfigurationContainingAssemblyProvider<T> : IEntityTypeConfigurationContainingAssemblyProvider<T> where T : DbContext
{
    public Assembly Assembly { get; }

    public EntityTypeConfigurationContainingAssemblyProvider(Assembly assembly)
    {
        Assembly = assembly;
    }
}