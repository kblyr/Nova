namespace Nova;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddMapster(this IServiceCollection services)
    {
        var config = TypeAdapterConfig.GlobalSettings;
        return services
            .AddSingleton(config)
            .AddScoped<IMapper, ServiceMapper>();
    }
}