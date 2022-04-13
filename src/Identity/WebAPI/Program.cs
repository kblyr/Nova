using Nova;
using Nova.Core;
using Nova.Core.Security;
using Nova.EFCore;
using Nova.EFCore.Postgres;
using Nova.Identity.Configuration;
using Nova.Identity.Core;
using Nova.Identity.Data;
using Nova.WebAPI.Host;
using Nova.WebAPI.Shared;

var builder = WebApplication.CreateBuilder(args);

builder.Nova(nova => nova
    .WithSecurity(security => security
        .AddStringDecryptorWithPemFile(builder.Configuration["Nova:Security:StringDecryption:KeyFilePath"])
    )
    .EFCore(efCore => efCore
        .Postgres(postgres => postgres
            .AddDbContextFactories(builder.Configuration.GetConnectionString("Postgres:Nova:Identity"), Nova.Identity.EFCore.Postgres.AssemblyMarker.Assembly)
                .For<UserDbContext>()
                .For<UserEmailAddressDbContext>()
                .For<UserPasswordLoginDbContext>()
        )
    )
    .WebAPI(webApi => webApi
        .Host(host => host
            .AddApiMediator()
            .AddResponseMapping()
        )
    )
    .Identity(identity => identity
        .Configure<UserConfig>(UserConfig.CONFIGKEY)
        .Configure<UserLoginTypesConfig>(UserLoginTypesConfig.CONFIGKEY)
        .Configure<UserStatusesConfig>(UserStatusesConfig.CONFIGKEY)
    )
);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
