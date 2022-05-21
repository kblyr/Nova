namespace Nova.Identity.Endpoints;

sealed class CreateEmailVerificationCodeEndpoint : ApiEndpoint<CreateEmailVerificationCode.Request, CreateEmailVerificationCodeCommand>
{
    public override void Configure()
    {
        AllowAnonymous();
        Post("email/verificationCode");
    }
}