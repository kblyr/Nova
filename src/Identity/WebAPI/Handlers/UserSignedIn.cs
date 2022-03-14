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
    readonly AccessTokenConfig _config;

    public UserSignedIn_Handler(IHttpContextAccessor contextAccessor, IMediator mediator, IMapper mapper, IOptions<AccessTokenConfig> config)
    {
        _contextAccessor = contextAccessor;
        _mediator = mediator;
        _mapper = mapper;
        _config = config.Value;
    }

    public async Task Handle(UserSignedIn notification, CancellationToken cancellationToken)
    {
        var context = _contextAccessor.HttpContext;

        if (context is null)
            return;

        var response_generateAccessToken = await _mediator.Send(new GenerateAccessToken(notification.UserId, notification.ApplicationId));
        
        if (response_generateAccessToken is not GenerateAccessToken.Response _response_generateAccessToken)
            return;

        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            MaxAge = _config.SessionExpiration
        };
        context.Response.Headers.TryAdd(HeaderNames.AccessToken, _response_generateAccessToken.AccessToken.TokenString);
        context.Response.Cookies.Append(CookieNames.SessionId, _response_generateAccessToken.AccessToken.Id, cookieOptions);
    }
}
