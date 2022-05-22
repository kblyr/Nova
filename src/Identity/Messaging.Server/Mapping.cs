namespace Nova.Identity;

sealed class Mapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.ForType<UserSignedUpEvent, CreateUserEmailVerificationCodeRequestedEvent>()
            .Map(dest => dest.UserId, src => src.Id);
    }
}