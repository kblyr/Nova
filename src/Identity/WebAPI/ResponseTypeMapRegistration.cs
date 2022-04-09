namespace Nova.Identity;

sealed class ResponseTypeMapRegistration : IResponseTypeMapRegistration
{
    public void Register(IResponseTypeMapRegistry registry)
    {
        registry
            .RegisterCreated<AddUserCommand.Response, AddUser.Response>()
            .RegisterCreated<AddUserPasswordLoginCommand.Response, AddUserPasswordLogin.Response>()
            .RegisterNotFound<UserNotFoundResponse, UserNotFound.Response>()
            .RegisterConflict<UserPasswordLoginAlreadyExistsResponse, UserPasswordLoginAlreadyExists.Response>()
            .RegisterNotFound<UserStatusNotFoundResponse, UserStatusNotFound.Response>()
            ;
    }
}
