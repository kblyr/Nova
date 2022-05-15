using System.Reflection;

namespace Nova;

public interface IEntityConfigAssemblyProvider<T> where T : DbContext
{
    Assembly Assembly { get; }
}