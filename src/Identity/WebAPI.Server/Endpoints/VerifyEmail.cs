namespace Nova.Identity.Endpoints;

sealed class VerifyEmailEndpoint : ApiEndpoint<VerifyEmail.Request, VerifyEmailCommand>
{
    public override void Configure()
    {
        AllowAnonymous();
        Post("email/verify");
    }
}