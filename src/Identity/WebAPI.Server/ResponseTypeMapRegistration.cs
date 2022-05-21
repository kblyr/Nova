using Microsoft.AspNetCore.Http;

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
            ;
    }
}