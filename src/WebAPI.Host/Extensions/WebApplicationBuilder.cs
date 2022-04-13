using Microsoft.AspNetCore.Builder;
using Nova.Core;

namespace Nova;

public static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder Nova(this WebApplicationBuilder builder, InjectDependencies<DependencyInjector>? injectDependencies = null)
    {
        builder.Services.Nova(builder.Configuration, injectDependencies);
        return builder;
    }
}