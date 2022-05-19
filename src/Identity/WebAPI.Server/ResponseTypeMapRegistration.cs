using Microsoft.AspNetCore.Http;

namespace Nova.Identity;

sealed class ResponseTypeMapRegistration : IResponseTypeMapRegistration
{
    public void Register(IResponseTypeMapRegistry registry)
    {
        registry
            .Register<CreateEmailVerificationCodeCommand.Response, CreateEmailVerificationCode.Response>(StatusCodes.Status201Created)
            .Register<EmailVerificationCodeAlreadyCreatedResponse, EmailVerificationCodeAlreadyCreated.Response>(StatusCodes.Status400BadRequest);
    }
}