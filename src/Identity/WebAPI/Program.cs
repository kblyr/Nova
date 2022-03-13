using System.Security.Cryptography;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Nova.Core;
using Nova.EFCore;
using Nova.Identity.Core;
using Nova.Identity.Core.Utilities;
using Nova.Identity.EFCore;
using Nova.Identity.EFCore.Postgres;
using Nova.Identity.Redis;
using Nova.Web;
using Nova.Web.Auditing;
using Nova.Web.Messaging;

var builder = WebApplication.CreateBuilder(args);

var assemblyMarkers = new []
{
    Nova.Core.AssemblyMarker.Assembly,
    Nova.Identity.EFCore.AssemblyMarker.Assembly,
    Nova.Identity.Redis.AssemblyMarker.Assembly,
    Nova.Identity.WebAPI.AssemblyMarker.Assembly
};

builder.Services
    .AddAuthentication(options => {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options => {
        var publicKeyXmlString = File.ReadAllText(@"C:\Crypto Keys\Nova\public_key.xml");
        using var rsa = RSA.Create();
        rsa.FromXmlString(publicKeyXmlString);
        var securityKey = new RsaSecurityKey(rsa);

        options.SaveToken = true;
        options.TokenValidationParameters = new()
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = securityKey
        };
    });

builder.Services
    .AddMediatR(assemblyMarkers)
    .AddAutoMapper(assemblyMarkers)
    .AddResponseMapping(Nova.Identity.WebAPI.AssemblyMarker.Assembly)
    .AddHttpContextAccessor();

builder.Services.AddNova(nova => nova
    .EFCore(efCore => efCore
        .AddDbContextFactory<Nova.Identity.DatabaseContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("Postgres:Nova:Identity")))
    )
    .Web(web => web
        .AddAuditing()
        .AddMessaging()
    )
    .Identity(identity => identity
        .SetupConfigurations(builder.Configuration)
        .AddUtilities()
        .EFCore(efCore => efCore
            .Postgres(postgres => postgres.AddDefault())
        )
        .Redis(redis =>
            redis.AddConnectionMultiplexerFactory(builder.Configuration.GetConnectionString("Redis:Nova:Identity")) 
        )
    )
);

builder.Services.AddSingleton<Nova.Identity.Utilities.TokenGenerator>();

var app = builder.Build();
app.UseAuthentication();
// app.UseAuthorization();
app.MapEndpoints(Nova.Identity.WebAPI.AssemblyMarker.Assembly);
app.Run();
