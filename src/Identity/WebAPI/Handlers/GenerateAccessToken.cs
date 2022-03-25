using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Nova.Authentication;
using Nova.Identity.Configuration;
using Nova.Identity.Contracts;

namespace Nova.Identity.Handlers;

sealed class GenerateAccessToken_Handler : Messaging.RequestHandler<GenerateAccessToken>
{
    readonly IMediator _mediator;
    readonly IMapper _mapper;
    readonly AccessTokenConfig _config;

    public GenerateAccessToken_Handler(IMediator mediator, IMapper mapper, IOptions<AccessTokenConfig> config)
    {
        _mediator = mediator;
        _mapper = mapper;
        _config = config.Value;
    }

    public async Task<Response> Handle(GenerateAccessToken request, CancellationToken cancellationToken)
    {
        var response_getAccessTokenPayload = await _mediator.Send(_mapper.Map<GenerateAccessToken, GetAccessTokenPayload>(request));

        if (response_getAccessTokenPayload is not GetAccessTokenPayload.Response _response_getAccessTokenPayload)
            return response_getAccessTokenPayload;

        if (string.IsNullOrWhiteSpace(_config.PrivateSigningKeyPath) || File.Exists(_config.PrivateSigningKeyPath) == false)
            throw new FileNotFoundException("Private Signing Key cannot be found", _config.PrivateSigningKeyPath);
            
        var id = Guid.NewGuid().ToString("N");
        var tokenString = await CreateTokenString(_config, _response_getAccessTokenPayload, _mapper);
        
        await _mediator.Publish(new AccessTokenGenerated(
            request.UserId, 
            request.ApplicationId,
            new(id, tokenString)
        ));
        return new GenerateAccessToken.Response(new(id, tokenString));
    }

    static async Task<string> CreateTokenString(AccessTokenConfig config, GetAccessTokenPayload.Response response, IMapper mapper)
    {
        var keyXmlString = await File.ReadAllTextAsync(config.PrivateSigningKeyPath);
        using var rsa = RSA.Create();
        var signingCredentials = new SigningCredentials(new RsaSecurityKey(rsa), SecurityAlgorithms.RsaSha256);
        var descriptor = new SecurityTokenDescriptor
        {
            Expires = DateTime.UtcNow + config.Expiration,
            SigningCredentials = signingCredentials,
            Claims = GetClaims(response, mapper)
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var securityToken = tokenHandler.CreateToken(descriptor);
        return tokenHandler.WriteToken(securityToken);
    }

    static IDictionary<string, object> GetClaims(GetAccessTokenPayload.Response response, IMapper mapper)
    {
        var claims = new Dictionary<string, object>();
        claims.Add(Session.ClaimType, mapper.Map<GetAccessTokenPayload.Response, Session>(response));
        return claims;
    }
}
