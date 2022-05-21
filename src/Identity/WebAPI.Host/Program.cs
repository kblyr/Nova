using FastEndpoints;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Nova;
using Nova.Core;
using Nova.EFCore;
using Nova.Identity.Consumers;
using Nova.Identity.Contexts;
using Nova.Identity.Contracts;
using Nova.Identity.KeyGenerators;
using Nova.Identity.Senders;
using Nova.Identity.TemplateLoaders;
using Nova.Mailing;
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
    Nova.Identity.Mailing.AssemblyMarker.Assembly,
    Nova.Identity.Messaging.Publisher.AssemblyMarker.Assembly,
    Nova.Identity.Redis.AssemblyMarker.Assembly
};

var responseTypeMapAssemblies = new[]
{
    Nova.Identity.WebAPI.Server.AssemblyMarker.Assembly
};

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseDefaultServiceProvider(options => options.ValidateScopes = false);
builder.Services
    .AddFastEndpoints(endpointAssemblies)
    .AddMediatR(cqrsHandlerAssemblies)
    .AddMapster()
    .AddMassTransit(massTransit => {
        massTransit.AddRequestClient<CreateEmailVerificationCodeCommand>();
        massTransit.AddConsumer<SendEmailVerificationCodeRequestedConsumer>();
        massTransit.UsingRabbitMq((context, rabbitMq) => {
            rabbitMq.Host(builder.Configuration.GetConnectionString("RabbitMQ:Nova:Identity"));
            rabbitMq.ConfigureEndpoints(context);
        });
    });

builder.Services
    .AddNova()
    .AddNovaEFCore(efCore => efCore
        .AddDbContextFactory<IdentityDbContext>(Nova.Identity.EFCore.PostgreSQL.AssemblyMarker.Assembly, options => options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSQL:Nova:Identity")))
    )
    .AddNovaMailing(mailing => mailing
        .AddSender<EmailVerificationCodeSender>()
        .AddTemplateLoaderFromFile<EmailVerificationCodeTemplateLoader>()
    )
    .AddNovaMessagingPublisher()
    .AddNovaRedis(redis => redis
        .AddMultiplexerProvider<Nova.Identity.MultiplexerProvider>(builder.Configuration.GetConnectionString("Redis:Nova:Identity"))
        .AddKeyGenerator<EmailVerificationCodeKeyGenerator>()
    )
    .AddNovaWebAPI()
    .AddNovaWebAPIServer(responseTypeMapAssemblies)
    .Configure<Nova.Identity.Configuration.EmailVerificationCodeMailOptions>(builder.Configuration.GetSection(Nova.Identity.Configuration.EmailVerificationCodeMailOptions.CONFIGKEY));

var app = builder.Build();
app.UseAuthorization();
app.UseFastEndpoints();
app.Run();
