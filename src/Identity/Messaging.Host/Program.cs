using MassTransit;
using Nova.Sagas;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services => {
        services.AddMassTransit(massTransit => {
            massTransit.AddSagaStateMachine<EmailVerificationSaga, EmailVerificationSaga.Instance>()
                .Endpoint(config => config.Name = "Nova_Identity_EmailVerification")
                .MongoDbRepository("mongodb://mongo:mongo@localhost:27017/?authSource=admin&readPreference=primary", config => {
                    config.DatabaseName = "nova_identity";
                    config.CollectionName = "email_verification";
                });

            massTransit.UsingRabbitMq((context, rabbitMq) => {
                rabbitMq.Host("rabbitmq://localhost:5672");
                rabbitMq.ConfigureEndpoints(context);
            });
        });
    })
    .Build();

await host.RunAsync();
