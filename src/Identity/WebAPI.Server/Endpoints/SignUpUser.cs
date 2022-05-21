namespace Nova.Identity.Endpoints;

sealed class SignUpUserEndpoint : ApiEndpoint<SignUpUser.Request, SignUpUserCommand>
{
    public override void Configure()
    {
        AllowAnonymous();
        Post("user/sign-up");
    }
}