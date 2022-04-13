namespace Nova.Identity.Endpoints;

public interface UserEndpoints
{
    [Post(AddUser.ROUTE)]
    Task<IApiResponse<AddUser.Response>> Add([Body]AddUser.Request request);

    [Post(AddPasswordLoginToUser.ROUTE)]
    Task<IApiResponse<AddPasswordLoginToUser.Response>> Add(string id, [Body]AddPasswordLoginToUser.Request request);

    [Post(AddEmailAddressToUser.ROUTE)]
    Task<IApiResponse<AddEmailAddressToUser.Response>> Add(string id, [Body]AddEmailAddressToUser.Request request);
}