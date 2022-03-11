namespace Nova.Identity;

sealed class ResponseTypeRegistration : IResponseTypeRegistration
{
    public void Register(ResponseTypeRegistry registry)
    {
        registry
            .RegisterCreated<Contracts.AddUser.Response, AddUser.Response>()
            .RegisterConflict<Contracts.UsernameAlreadyExists>()
            .RegisterNotFound<Contracts.UserStatusNotFound>();
    }
}
