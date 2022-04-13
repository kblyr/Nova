namespace Nova.Identity.Controllers;

[ApiController]
[Route(ControllerRoutes.User)]
public sealed class UserController : ApiControllerBase
{
    [HttpPost(ActionRoutes.User.Add)]
    public async Task<IActionResult> Add([FromBody]AddUser.Request request)
    {
        return await Mediator.SendThenMap<AddUser.Request, AddUserCommand>(request);
    }

    [HttpPost(ActionRoutes.User.AddPasswordLogin)]
    public Task<IActionResult> AddPasswordLogin(string id, [FromBody]AddPasswordLoginToUser.Request request)
    {
        return Mediator.SendThenMap<AddPasswordLoginToUser.Request, AddPasswordLoginToUserCommand>(request, request => request with { UserId = Hashids.DecodeFirstOrDefault(id) });
    }

    [HttpPost(ActionRoutes.User.AddEmailAddress)]
    public Task<IActionResult> AddEmailAddress(string id, [FromBody]AddEmailAddressToUser.Request request)
    {
        return Mediator.SendThenMap<AddEmailAddressToUser.Request, AddEmailAddressToUserCommand>(request, request => request with { UserId = Hashids.DecodeFirstOrDefault(id) });
    }
}