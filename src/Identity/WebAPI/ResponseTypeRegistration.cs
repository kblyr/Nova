namespace Nova.Identity;

sealed class ResponseTypeRegistration : IResponseTypeRegistration
{
    public void Register(ResponseTypeRegistry registry)
    {
        registry
            .RegisterCreated<Contracts.AddUser.Response, AddUser.Response>()
            .RegisterConflict<Contracts.UsernameAlreadyExists>()
            .RegisterNotFound<Contracts.UserStatusNotFound>()
            .RegisterOK<Contracts.IdentifyUserForSignIn.Response, IdentifyUserForSignIn.Response>()
            .RegisterNotFound<Contracts.ApplicationNotFound>()
            .RegisterNotFound<Contracts.UsernameNotFound>()
            .RegisterForbidden<Contracts.UserNotActive>()
            .RegisterForbidden<Contracts.UserNotLinkedToApplication>()
            .RegisterOK<Contracts.SignInUserWithPassword.Response, SignInUserWithPassword.Response>()
            .RegisterBadRequest<Contracts.IncorrectUserPassword>()
            .RegisterNotFound<Contracts.UserNotFound>();
    }
}
