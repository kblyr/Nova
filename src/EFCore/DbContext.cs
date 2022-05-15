using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Nova;

public abstract class DbContextBase<T> : DbContext where T : DbContext
{
    protected DbContextBase(DbContextOptions<T> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        TryApplyEntityConfigFromAssembly(modelBuilder);
    }

    void TryApplyEntityConfigFromAssembly(ModelBuilder builder)
    {
        var services = this.GetInfrastructure<IServiceProvider>();

        var assemblyProvider = services.GetService<IEntityConfigAssemblyProvider<T>>();
        if (assemblyProvider is not null)
            builder.ApplyConfigurationsFromAssembly(assemblyProvider.Assembly);
    }
}