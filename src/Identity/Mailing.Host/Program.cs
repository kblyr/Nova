using MassTransit;
using MediatR;
using Nova.Identity.Configuration;
using Nova.Identity.Consumers;
using Nova.Identity.Senders;
using Nova.Identity.TemplateLoaders;
using Nova.Mailing;
using Nova.Messaging.Publisher;

var cqrsHandlerAssemblies = new[]
{
    Nova.Identity.Mailing.AssemblyMarker.Assembly,
    Nova.Identity.Messaging.Publisher.AssemblyMarker.Assembly
};

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((host, services) => {
        services.AddMediatR(cqrsHandlerAssemblies);
        services.AddMassTransit(massTransit => {
            massTransit.AddConsumer<SendEmailVerificationCodeRequestedConsumer>();
            massTransit.UsingRabbitMq((context, rabbitMq) => {
                rabbitMq.Host(host.Configuration["Nova:Identity:ConnectionStrings:RabbitMQ"]);
                rabbitMq.ConfigureEndpoints(context);
            });
        });

        services
            .AddNovaMailing(mailing => mailing
                .AddSender<EmailVerificationCodeSender>()
                .AddTemplateLoaderFromFile<EmailVerificationCodeTemplateLoader>()
            )
            .AddNovaMessagingPublisher();

        services.Configure<EmailVerificationCodeMailOptions>(host.Configuration.GetSection(EmailVerificationCodeMailOptions.CONFIGKEY));
    })
    .Build();

await host.RunAsync();