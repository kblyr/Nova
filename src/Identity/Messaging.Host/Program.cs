using MassTransit;
using Nova.Identity.Sagas;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services => {
        services.AddMassTransit(massTransit => {
            massTransit.AddSagaStateMachine<UserSaga, UserSaga.Instance>()
                .Endpoint(config => config.Name = "Nova_Identity_User")
                .MongoDbRepository("mongodb://mongo:mongo@localhost:27017/?authSource=admin&readPreference=primary", config => {
                    config.DatabaseName = "nova_identity";
                    config.CollectionName = "user";
                });

            massTransit.AddSagaStateMachine<UserEmailVerificationSaga, UserEmailVerificationSaga.Instance>()
                .Endpoint(config => config.Name = "Nova_Identity_UserEmailVerification")
                .MongoDbRepository("mongodb://mongo:mongo@localhost:27017/?authSource=admin&readPreference=primary", config => {
                    config.DatabaseName = "nova_identity";
                    config.CollectionName = "user_email_verification";
                });

            massTransit.UsingRabbitMq((context, rabbitMq) => {
                rabbitMq.Host("rabbitmq://localhost:5672");
                rabbitMq.ConfigureEndpoints(context);
            });
        });
    })
    .Build();

await host.RunAsync();
