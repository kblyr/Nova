using MediatR;
using Nova.Core;

var builder = WebApplication.CreateBuilder(args);

var assemblyMarkers = new[] 
{
    Nova.Core.AssemblyMarker.Assembly,
    Nova.HRIS.Core.AssemblyMarker.Assembly,
    Nova.HRIS.EFCore.AssemblyMarker.Assembly,
    Nova.HRIS.WebAPI.AssemblyMarker.Assembly
};

builder.Services
    .AddMediatR(assemblyMarkers)
    .AddAutoMapper(assemblyMarkers)
    .AddResponseMapping(Nova.HRIS.WebAPI.AssemblyMarker.Assembly);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
