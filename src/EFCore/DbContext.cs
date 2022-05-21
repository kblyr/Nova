using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Nova;

public abstract class DbContextBase<T> : DbContext where T : DbContext
{
    readonly IEntityConfigAssemblyProvider<T>? _entityConfigAssemblyProvider;

    protected DbContextBase(DbContextOptions<T> options) : base(options)
    {
    }

    protected DbContextBase(DbContextOptions<T> options, IEntityConfigAssemblyProvider<T> entityConfigAssemblyProvider) : base(options)
    {
        _entityConfigAssemblyProvider = entityConfigAssemblyProvider;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        TryApplyEntityConfigFromAssembly(modelBuilder);
    }

    void TryApplyEntityConfigFromAssembly(ModelBuilder builder)
    {
        if (_entityConfigAssemblyProvider is not null)
        {
            builder.ApplyConfigurationsFromAssembly(_entityConfigAssemblyProvider.Assembly);
        }
    }
}