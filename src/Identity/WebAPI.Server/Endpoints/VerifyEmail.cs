namespace Nova.Identity.Endpoints;

sealed class VerifyEmailEndpoin : ApiEndpoint<VerifyEmail.Request, VerifyEmailCommand>
{
    public override void Configure()
    {
        AllowAnonymous();
        Post("email/verify");
    }
}