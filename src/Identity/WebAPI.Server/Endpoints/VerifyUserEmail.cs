namespace Nova.Identity.Endpoints;

sealed class VerifyUserEmailEndpoint : ApiEndpoint<VerifyUserEmail.Request, VerifyUserEmailCommand>
{
    public override void Configure()
    {
        AllowAnonymous();
        Post("user/{id}/verify-email/{emailAddress}");
    }
}