using MassTransit;
using Nova.Identity.Sagas;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((host, services) => {
        services.AddMassTransit(massTransit => {
            massTransit.AddSagaStateMachine<EmailVerificationSaga, EmailVerificationSaga.Instance>()
                .Endpoint(config => config.Name = "Nova_Identity_EmailVerification")
                .MongoDbRepository(host.Configuration.GetConnectionString("MongoDB:Nova:Identity"), config => {
                    config.DatabaseName = "nova_identity";
                    config.CollectionName = "email_verification";
                });

            massTransit.UsingRabbitMq((context, rabbitMq) => {
                rabbitMq.Host(host.Configuration.GetConnectionString("RabbitMQ:Nova:Identity"));
                rabbitMq.ConfigureEndpoints(context);
            });
        });
    })
    .Build();

await host.RunAsync();
