using AutoMapper;
using MediatR;
using Microsoft.Extensions.Options;
using Nova.Identity.Authentication;
using Nova.Identity.Configuration;
using Nova.Identity.Contracts;

namespace Nova.Identity.Handlers;

sealed class UserSignedIn_Handler : INotificationHandler<UserSignedIn>
{
    readonly IHttpContextAccessor _contextAccessor;
    readonly IMediator _mediator;
    readonly IMapper _mapper;
    readonly RefreshTokenConfig _refreshTokenConfig;

    public UserSignedIn_Handler(IHttpContextAccessor contextAccessor, IMediator mediator, IMapper mapper, IOptions<RefreshTokenConfig> refreshTokenConfig)
    {
        _contextAccessor = contextAccessor;
        _mediator = mediator;
        _mapper = mapper;
        _refreshTokenConfig = refreshTokenConfig.Value;
    }

    public async Task Handle(UserSignedIn notification, CancellationToken cancellationToken)
    {
        var context = _contextAccessor.HttpContext;

        if (context is null)
            return;

        var response_generateAccessToken = await _mediator.Send(new GenerateAccessToken(notification.UserId, notification.ApplicationId));
        
        if (response_generateAccessToken is not GenerateAccessToken.Response _response_generateAccessToken)
            return;

        var response_generateRefreshToken = await _mediator.Send(_mapper.Map<GenerateAccessToken.Response, GenerateRefreshToken>(_response_generateAccessToken));

        if (response_generateRefreshToken is not GenerateRefreshToken.Response _response_generateRefreshToken)
            return;

        context.Response.Headers.TryAdd(HeaderNames.AccessToken, _response_generateAccessToken.AccessToken.TokenString);
        context.Response.Cookies.Append(CookieNames.RefreshToken, _response_generateRefreshToken.RefreshToken, new CookieOptions
        {
            HttpOnly = true,
            MaxAge = _refreshTokenConfig.Expiration
        });
    }
}
