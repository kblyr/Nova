using FastEndpoints;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Nova;
using Nova.Core;
using Nova.EFCore;
using Nova.Identity.Contexts;
using Nova.Identity.Contracts;
using Nova.Identity.KeyGenerators;
using Nova.Messaging.Publisher;
using Nova.Redis;
using Nova.WebAPI;
using Nova.WebAPI.Server;

var endpointAssemblies = new[]
{
    Nova.Identity.WebAPI.Server.AssemblyMarker.Assembly
};

var cqrsHandlerAssemblies = new[]
{
    Nova.Identity.Messaging.Publisher.AssemblyMarker.Assembly,
    Nova.Identity.Redis.AssemblyMarker.Assembly
};

var responseTypeMapAssemblies = new[]
{
    Nova.Identity.WebAPI.Server.AssemblyMarker.Assembly
};

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddFastEndpoints(endpointAssemblies)
    .AddMediatR(cqrsHandlerAssemblies)
    .AddMapster()
    .AddMassTransit(massTransit => {
        massTransit.AddRequestClient<CreateEmailVerificationCodeCommand>();
        massTransit.UsingRabbitMq((context, rabbitMq) => {
            rabbitMq.Host(builder.Configuration["Nova:Identity:ConnectionStrings:RabbitMQ"]);
            rabbitMq.ConfigureEndpoints(context);
        });
    });

builder.Services
    .AddNova()
    .AddNovaEFCore(efCore => efCore
        .AddDbContextFactory<IdentityDbContext>(Nova.Identity.EFCore.PostgreSQL.AssemblyMarker.Assembly, options => options.UseNpgsql(builder.Configuration["Nova:Identity:ConnectionStrings:PostgreSQL"]))
    )
    .AddNovaMessagingPublisher()
    .AddNovaRedis(redis => redis
        .AddMultiplexerProvider<Nova.Identity.MultiplexerProvider>(builder.Configuration["Nova:Identity:ConnectionStrings:Redis"])
        .AddKeyGenerator<EmailVerificationCodeKeyGenerator>()
    )
    .AddNovaWebAPI()
    .AddNovaWebAPIServer(responseTypeMapAssemblies);

var app = builder.Build();
app.UseAuthorization();
app.UseFastEndpoints();
app.Run();
