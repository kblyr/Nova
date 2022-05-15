namespace Nova.Identity;

sealed class Mapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.ForType<AddUserCommand.Response, AddUser.Response>()
            .Map(dest => dest.Id, src => MapConverters.UserId.Convert(src.Id));
    }
}
