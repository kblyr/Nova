namespace Nova.Identity.Endpoints;

public sealed class AddUserEndpoint : ApiEndpoint<AddUser.Request, AddUserCommand>
{
    public override void Configure()
    {
        Post(ApiRoutes.User.Add);
    }
}