using MassTransit;
using Nova.Identity.Sagas;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services => {
        services.AddMassTransit(massTransit => {
            massTransit.AddSagaStateMachine<UserSaga, UserSaga.Instance>()
                .MongoDbRepository("mongodb://mongo:mongo@localhost:27017/?authSource=admin&readPreference=primary", config => {
                    config.DatabaseName = "nova_identity";
                    config.CollectionName = "user";
                });

            massTransit.UsingRabbitMq((context, rabbitMq) => {
                rabbitMq.Host("rabbitmq://localhost:5672");
                rabbitMq.ConfigureEndpoints(context);
            });
        });
    })
    .Build();

await host.RunAsync();
