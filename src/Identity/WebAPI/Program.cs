using System.Security.Cryptography;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Nova.Core;
using Nova.Core.Auditing;
using Nova.Core.Utilities;
using Nova.Core.Validation;
using Nova.EFCore;
using Nova.Identity.Core;
using Nova.Identity.Core.Utilities;
using Nova.Identity.EFCore;
using Nova.Identity.EFCore.Postgres;
using Nova.Redis;
using Nova.Validation;
using Nova.Web;
using Nova.Web.Authentication;
using Nova.Web.Messaging;

var builder = WebApplication.CreateBuilder(args);

var assemblyMarkers = new []
{
    Nova.Core.AssemblyMarker.Assembly,
    Nova.Identity.Core.AssemblyMarker.Assembly,
    Nova.Identity.EFCore.AssemblyMarker.Assembly,
    Nova.Identity.Redis.AssemblyMarker.Assembly,
    Nova.Identity.WebAPI.AssemblyMarker.Assembly
};

var usePostgres = void (DbContextOptionsBuilder options) => options.UseNpgsql(builder.Configuration.GetConnectionString("Postgres:Nova:Identity"));

builder.Services
    .AddAuthentication(options => {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer();

builder.Services.AddAuthorization();
builder.Services
    .AddTransient(provider => RSA.Create())
    .AddTransient<SecurityKey>(provider =>
    {
        var rsa = provider.GetRequiredService<RSA>();
        // var publicKeyXmlString = File.ReadAllText(@"C:\Crypto Keys\Nova\private_key.xml");
        // rsa.FromXmlString(publicKeyXmlString);
        rsa.ImportFromPem(@"-----BEGIN PRIVATE KEY-----
MIIEugIBADANBgkqhkiG9w0BAQEFAASCBKQwggSgAgEAAoIBAQDOwfCPm3dc+HrQ
8NulaPltmeYZvFcupL4zq7wzKJK0Ec7C9dAzrJ64HqroJrT5y58wp9bo8pQjhgKU
skgosEJA8tkK/vLMp6YMv3kww5WiA4iRS050mYqqbSublnz4lYIAQhjLYYNoMm7s
tF464kYbl7vWcv+vDBVgT92J3brnavQeaGoPiKif/b400u5YjvS4OATZ8NvnuZn5
w4VtYAQUBQ3+rRNRoPylQfPhWBNo+JwIfteBuxrGcWeJo66Lgq3vDElJTWJnlIoy
4yKl2TIU0iKNz3BJbS2SKn7f5va5KyzUSIuuLuCujWFKeFjB0QCsPlXkPxnjbeDk
FCz+3MKxAgMBAAECgf81IFdusmPmlLBAzSdK7l/0FcSl3t80IFN0JjjBu+fbyUlC
j0GQTDOAykYMJU/vKq9ALOkpsisrqokOrmzeaLtfp7d345FIpg2DoMWYH+dANrwU
kJE+3hY7pP/xu8Eg3JBot79IVwLKwjOUgjJ64+CdnilihhZAsrooGAUwaMW5Y0ZJ
w1ZPct1vk31pz/O7siyZRW1uyhSI0oG6iF4rGh025eac0XwD5oPitL+kHVEVsgJ+
AVhUqKNOuHCJ1h2iphM1ob7ilUrKJy+BgU/TPnxhATfAg+WkL5dW3TJF2Ge9M1zH
RzV0AVyOIFCYhFkzgAu92NcNGuTU22TT2ck61wECgYEA5a7PO8o8sTHVekIxs1nZ
T+yuj5AtTBHViZfWLm+Zhu0PRVZhb7nPlu93W+eQ7uk9VZp8TgS+H8YMjZbSXVsA
KJIudvKERhmfRCNcsKt7hBtDubww8rhUjep0J0lEOKyJdSq+H5DkWkco27KYH3tJ
Oo+nKg83d923LnhJQJASboECgYEA5nKsPFxwzzNoA3B9N//tnM3UVuueXzhRpO0j
dzAmYZ4l3VmGFzcbPS0fK4SNNzc79LHBWJ/QkMfgk6CnfBM/oFW0LTngujsU49AU
MnVGejcwmPg71AkMcjKuXpIg813c0pmlCxPz94yYrWQxbfRvw492MWlhsUQYfUPO
Tl5/nDECgYAYycoB2Oy97vPsMvKsOZkzgAbC5buNxyr1o5ZxAoheQH1ybUUsyq/1
yThnidFhh3igIYEi5m4ifbxjF5DylFyrBEPRgJD4A9Hlh698PbWh51Ni20WpHG27
tz778nNTboOTSp6kR33tpFprg5XZZ/PaRyCycFv7KsmXUVIOjr3+gQKBgDN28Jga
XOcpJ9V8zbov4bTfEsjGnfvN7A3VF9KKYkfytaoVUCjnGaIz7X4egBsQrsca238b
UczDlpfhjQXclp0MBs0C2/k7MJMf6SLLpg2tPaEr7tCPpMPsJZzhLZKsJ6Cwx3cN
4bIrJ/2xHojbygn2hALM8hBQkNeIyQ4fdIhBAoGALKQoVknraosGbg3ytBIzXgtj
wasy7Rknxrdnd9ECPyB2qfjqu/ajGWabdeHIEfKgHHwYl5hdFiM3ltFcP1u8wcwB
IT6SKw+nsF6V2CeVoRRXrtAOlgZmq2MHkEe9T6SDYP1TP5VwLc/kHzVE1AHfCiwu
yxWSL4BCWh/Wr5+td6o=
-----END PRIVATE KEY-----");
        Console.WriteLine("Getting one...");
        return new RsaSecurityKey(rsa);
    });

builder.Services
    .AddOptions<JwtBearerOptions>(JwtBearerDefaults.AuthenticationScheme)
    .Configure<SecurityKey>((options, signingKey) => {
        options.IncludeErrorDetails = true;
        options.SaveToken = true;
        options.TokenValidationParameters = new()
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = false,
            IssuerSigningKey = signingKey
        };
        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = async (context) => {
                Console.WriteLine(context.Exception.Message);
            },
            OnTokenValidated = async (context) => {
                Console.WriteLine(context);
            }
        };
    });

builder.Services
    .AddMediatR(assemblyMarkers)
    .AddAutoMapper(assemblyMarkers)
    .AddAccessValidators(Nova.Core.AssemblyMarker.Assembly)
    .AddAccessValidationConfigurations(Nova.Identity.Core.AssemblyMarker.Assembly)
    .AddResponseMapping(Nova.Identity.WebAPI.AssemblyMarker.Assembly)
    .AddHttpContextAccessor();

builder.Services.AddNova(nova => nova
    .AddAuditing()
    .AddUtilities()
    .AddValidation()
    .EFCore(efCore => efCore
        .AddDbContextFactory<Nova.Identity.AccessTokenDbContext>(usePostgres)
        .AddDbContextFactory<Nova.Identity.PermissionDbContext>(usePostgres)
        .AddDbContextFactory<Nova.Identity.RoleDbContext>(usePostgres)
        .AddDbContextFactory<Nova.Identity.UserDbContext>(usePostgres)
        .AddDbContextFactory<Nova.Identity.UserApplicationDbContext>(usePostgres)
        .AddDbContextFactory<Nova.Identity.UserStatusDbContext>(usePostgres)
    )
    .Web(web => web
        .AddAuthentication()
        .AddMessaging()
    )
    .Redis(redis => redis
        .AddMultiplexerProvider<Nova.Identity.MultiplexerProvider>(builder.Configuration.GetConnectionString("Redis:Nova:Identity"))
    )
    .Identity(identity => identity
        .SetupConfigurations(builder.Configuration)
        .AddUtilities()
        .EFCore(efCore => efCore
            .Postgres(postgres => postgres.AddDefault())
        )
    )
);

var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();
app.MapEndpoints(Nova.Identity.WebAPI.AssemblyMarker.Assembly);
app.Run();
