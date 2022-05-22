namespace Nova.Identity;

sealed class Mapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.ForType<IncorrectUserEmailVerificationCodeResponse, IncorrectUserEmailVerificationCode.Response>()
            .Map(dest => dest.Id, src => MapConverters.UserId.Convert(src.Id));

        config.ForType<SignUpUserCommand.Response, SignUpUser.Response>()
            .Map(dest => dest.Id, src => MapConverters.UserId.Convert(src.Id));

        config.ForType<UserEmailAddressNotFoundResponse, UserEmailAddressNotFound.Response>()
            .Map(dest => dest.Id, src => MapConverters.UserId.Convert(src.Id));

        config.ForType<VerifyUserEmail.Request, VerifyUserEmailCommand>()
            .Map(dest => dest.Id, src => MapConverters.UserId.Convert(src.Id));
    }
}
