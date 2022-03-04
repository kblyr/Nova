using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace Nova;

public interface IEntityTypeConfigurationContainingAssemblyProvider<TDbContext> where TDbContext : DbContext
{
    Assembly Assembly { get; }
}