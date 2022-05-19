namespace Nova.Identity.Endpoints;

public sealed class CreateEmailVerificationCodeEndpoint : ApiEndpoint<CreateEmailVerificationCode.Request, CreateEmailVerificationCodeCommand>
{
    public override void Configure()
    {
        AllowAnonymous();
        Post("email/verificationCode");
    }
}