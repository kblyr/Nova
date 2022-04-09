using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace Nova;

public interface IEntityTypeConfigurationContainingAssemblyProvider<T> where T : DbContext
{
    Assembly Assembly { get; }
}