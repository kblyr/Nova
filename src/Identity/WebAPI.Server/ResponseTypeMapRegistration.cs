namespace Nova.Identity;

sealed class ResponseTypeMapRegistration : IResponseTypeMapRegistration
{
    public void Register(IResponseTypeMapRegistry registry)
    {
        registry
            .RegisterCreated<CreateEmailVerificationCodeCommand.Response, CreateEmailVerificationCode.Response>()
            .RegisterNotFound<EmailAddressNotFoundResponse, EmailAddressNotFound.Response>()
            .RegisterBadRequest<EmailVerificationCodeAlreadyCreatedResponse, EmailVerificationCodeAlreadyCreated.Response>()
            .RegisterBadRequest<IncorrectEmailVerificationCodeResponse, IncorrectEmailVerificationCode.Response>()
            .RegisterBadRequest<IncorrectUserEmailVerificationCodeResponse, IncorrectUserEmailVerificationCode.Response>()
            .RegisterCreated<SignUpUserCommand.Response, SignUpUser.Response>()
            .RegisterBadRequest<UserEmailAddressAlreadyExistsResponse, UserEmailAddressAlreadyExists.Response>()
            .RegisterNotFound<UserEmailAddressNotFoundResponse, UserEmailAddressNotFound.Response>()
            .RegisterOK<VerifyEmailCommand.Response, VerifyEmail.Response>()
            .RegisterOK<VerifyUserEmailCommand.Response, VerifyUserEmail.Response>()
            ;
    }
}