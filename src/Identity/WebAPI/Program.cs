using MediatR;
using Microsoft.EntityFrameworkCore;
using Nova.Core;
using Nova.EFCore;
using Nova.Identity.Core;
using Nova.Identity.Core.Utilities;
using Nova.Identity.EFCore;
using Nova.Identity.EFCore.Postgres;
using Nova.Web;
using Nova.Web.Auditing;
using Nova.Web.Messaging;

var builder = WebApplication.CreateBuilder(args);

var assemblyMarkers = new []
{
    Nova.Core.AssemblyMarker.Assembly,
    Nova.Identity.EFCore.AssemblyMarker.Assembly,
    Nova.Identity.WebAPI.AssemblyMarker.Assembly
};

builder.Services
    .AddMediatR(assemblyMarkers)
    .AddAutoMapper(assemblyMarkers)
    .AddResponseMapping(Nova.Identity.WebAPI.AssemblyMarker.Assembly)
    .AddHttpContextAccessor();
builder.Services.AddNova(nova => nova
    .EFCore(efCore => efCore
        .AddDbContextFactory<Nova.Identity.DatabaseContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("Nova:Identity:V1")))
    )
    .Web(web => web
        .AddAuditing()
        .AddMessaging()
    )
    .Identity(identity => identity
        .AddUtilities()
        .EFCore(efCore => efCore
            .Postgres(postgres => postgres.AddDefault())
        )
    )
);

var app = builder.Build();
app.MapEndpoints(Nova.Identity.WebAPI.AssemblyMarker.Assembly);
app.Run();
