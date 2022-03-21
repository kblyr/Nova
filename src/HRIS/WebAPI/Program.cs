using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Nova.Core;
using Nova.Core.Utilities;
using Nova.EFCore;
using Nova.HRIS.Core;
using Nova.HRIS.EFCore;
using Nova.HRIS.EFCore.Postgres;
using Nova.Web;
using Nova.Web.Auditing;
using Nova.Web.Messaging;

var builder = WebApplication.CreateBuilder(args);
var usePostgres = void (DbContextOptionsBuilder options) => options.UseNpgsql(builder.Configuration.GetConnectionString("Postgres:Nova:HRIS"));

var assemblyMarkers = new[] 
{
    Nova.Core.AssemblyMarker.Assembly,
    Nova.HRIS.Core.AssemblyMarker.Assembly,
    Nova.HRIS.EFCore.AssemblyMarker.Assembly,
    Nova.HRIS.WebAPI.AssemblyMarker.Assembly
};

builder.Services
    .AddAuthentication(options => {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    });

builder.Services
    .AddMediatR(assemblyMarkers)
    .AddAutoMapper(assemblyMarkers)
    .AddResponseMapping(Nova.HRIS.WebAPI.AssemblyMarker.Assembly)
    .AddHttpContextAccessor();

builder.Services.AddNova(nova => nova
    .AddUtilities()
    .EFCore(efCore => efCore
        .AddDbContextFactory<Nova.HRIS.BarangayDbContext>(usePostgres)
        .AddDbContextFactory<Nova.HRIS.CityDbContext>(usePostgres)
        .AddDbContextFactory<Nova.HRIS.EmployeeDbContext>(usePostgres)
        .AddDbContextFactory<Nova.HRIS.ProvinceDbContext>(usePostgres)
    )
    .Web(web => web
        .AddAuditing()
        .AddMessaging()
    )
    .HRIS(hris => hris
        .EFCore(efCore => efCore
            .Postgres(postgres => postgres.AddDefault())
        )
    )
);

var app = builder.Build();
app.UseAuthentication();
app.MapEndpoints(Nova.HRIS.WebAPI.AssemblyMarker.Assembly);
app.Run();
