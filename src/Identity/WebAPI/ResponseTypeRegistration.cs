namespace Nova.Identity;

sealed class ResponseTypeRegistration : IResponseTypeRegistration
{
    public void Register(ResponseTypeRegistry registry)
    {
        registry
            .RegisterCreated<Contracts.AddRole.Response, AddRole.Response>()
            .RegisterCreated<Contracts.AddUser.Response, AddUser.Response>()
            .RegisterNotFound<Contracts.ApplicationNotFound>()
            .RegisterNotFound<Contracts.ApplicationNotInDomain>()
            .RegisterNotFound<Contracts.DomainNotFound>()
            .RegisterOK<Contracts.IdentifyUserForSignIn.Response, IdentifyUserForSignIn.Response>()
            .RegisterBadRequest<Contracts.IncorrectUserPassword>()
            .RegisterConflict<Contracts.RoleAlreadyExists>()
            .RegisterOK<Contracts.SignInUserWithPassword.Response, SignInUserWithPassword.Response>()
            .RegisterConflict<Contracts.UsernameAlreadyExists>()
            .RegisterNotFound<Contracts.UsernameNotFound>()
            .RegisterForbidden<Contracts.UserNotActive>()
            .RegisterNotFound<Contracts.UserNotFound>()
            .RegisterForbidden<Contracts.UserNotLinkedToApplication>()
            .RegisterNotFound<Contracts.UserStatusNotFound>()
            ;
    }
}
