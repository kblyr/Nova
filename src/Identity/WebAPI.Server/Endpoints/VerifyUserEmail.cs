namespace Nova.Identity.Endpoints;

sealed class VerifyUserEmailEndpoint : ApiEndpoint<VerifyUserEmail.Request, VerifyUserEmailCommand>
{
    public override void Configure()
    {
        AllowAnonymous();
        Post("user/{userId}/verify-email/{emailAddress}");
    }
}