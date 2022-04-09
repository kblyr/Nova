namespace Nova.Identity.Endpoints;

public interface UserEndpoints
{
    [Post(AddUser.ROUTE)]
    Task<IApiResponse<AddUser.Response>> Add([Body]AddUser.Request request);

    [Post(AddUserPasswordLogin.ROUTE)]
    Task<IApiResponse<AddUserPasswordLogin.Response>> Add(string id, [Body]AddUserPasswordLogin.Request request);

    [Post(AddUserEmailAddress.ROUTE)]
    Task<IApiResponse<AddUserEmailAddress.Response>> Add(string id, [Body]AddUserEmailAddress.Request request);
}