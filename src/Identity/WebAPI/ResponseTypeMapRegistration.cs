namespace Nova.Identity;

sealed class ResponseTypeMapRegistration : IResponseTypeMapRegistration
{
    public void Register(IResponseTypeMapRegistry registry)
    {
        registry
            .RegisterCreated<AddEmailAddressToUserCommand.Response, AddEmailAddressToUser.Response>()
            .RegisterCreated<AddPasswordLoginToUserCommand.Response, AddPasswordLoginToUser.Response>()
            .RegisterCreated<AddUserCommand.Response, AddUser.Response>()
            .RegisterNotFound<UserNotFoundResponse, UserNotFound.Response>()
            .RegisterConflict<UserPasswordLoginAlreadyExistsResponse, UserPasswordLoginAlreadyExists.Response>()
            .RegisterNotFound<UserStatusNotFoundResponse, UserStatusNotFound.Response>()
            ;
    }
}
