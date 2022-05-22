using MassTransit;
using Nova.Identity.Sagas;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((host, services) => {
        services.AddMassTransit(massTransit => {
            var mongoDbConnectionString = host.Configuration["Nova:Identity:ConnectionStrings:MongoDB"];
            massTransit.AddSagaStateMachine<EmailVerificationSaga, EmailVerificationSaga.Instance>()
                .Endpoint(config => config.Name = "Nova_Identity_EmailVerification")
                .MongoDbRepository(mongoDbConnectionString, config => {
                    config.DatabaseName = "nova_identity";
                    config.CollectionName = "email_verification";
                });
            massTransit.AddSagaStateMachine<UserEmailVerificationSaga, UserEmailVerificationSaga.Instance>()
                .Endpoint(config => config.Name = "Nova_Identity_UserEmailVerification")
                .MongoDbRepository(mongoDbConnectionString, config => {
                    config.DatabaseName = "nova_identity";
                    config.CollectionName = "user_email_verification";
                });

            massTransit.UsingRabbitMq((context, rabbitMq) => {
                rabbitMq.Host(host.Configuration["Nova:Identity:ConnectionStrings:RabbitMQ"]);
                rabbitMq.ConfigureEndpoints(context);
            });
        });
    })
    .Build();

await host.RunAsync();
