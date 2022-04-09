using DI.Nova;
using DI.Nova.Security;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddNova(nova => nova
    .AddSecurity(security => security
        .AddStringDecryptorWithPemFile(builder.Configuration["Nova:Security:StringDecryption:KeyFilePath"])
    )
);

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
