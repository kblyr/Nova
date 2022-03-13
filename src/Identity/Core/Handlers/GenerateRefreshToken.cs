using MediatR;

namespace Nova.Identity.Handlers;

sealed class GenerateRefreshToken_Handler : Messaging.RequestHandler<GenerateRefreshToken>
{
    readonly IMediator _mediator;

    public GenerateRefreshToken_Handler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<Response> Handle(GenerateRefreshToken request, CancellationToken cancellationToken)
    {
        var tokenString = Guid.NewGuid().ToString("D");
        await _mediator.Publish(new RefreshTokenGenerated(request.AccessToken.Id, tokenString));
        return new GenerateRefreshToken.Response(tokenString);
    }
}
