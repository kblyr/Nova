using Nova.Identity.Utilities;

namespace Nova.Identity.Behaviors;

sealed class IdentifyUserForSignIn_PostProcessor : RequestPostProcessor<Contracts.IdentifyUserForSignIn>
{
    readonly IHttpContextAccessor _contextAccessor;
    readonly TokenGenerator _tokenGenerator;

    public IdentifyUserForSignIn_PostProcessor(IHttpContextAccessor contextAccessor, TokenGenerator tokenGenerator)
    {
        _contextAccessor = contextAccessor;
        _tokenGenerator = tokenGenerator;
    }

    public Task Process(Contracts.IdentifyUserForSignIn request, Response response, CancellationToken cancellationToken)
    {
        // TODO:
        // Specify to the response header the authentication method specified by the user

        var context = _contextAccessor.HttpContext;

        if (context is not null)
        {
            context.Response.Headers.Add("NOVA_TOKEN", _tokenGenerator.Generate());
        }

        return Task.CompletedTask;
    }
} 