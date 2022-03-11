namespace Nova.Identity.Behaviors;

sealed class IdentifyUserForSignIn_PostProcessor : RequestPostProcessor<Contracts.IdentifyUserForSignIn>
{
    readonly IHttpContextAccessor _contextAccessor;

    public IdentifyUserForSignIn_PostProcessor(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }

    public Task Process(Contracts.IdentifyUserForSignIn request, Response response, CancellationToken cancellationToken)
    {
        // TODO:
        // Specify to the response header the authentication method specified by the user

        return Task.CompletedTask;
    }
} 