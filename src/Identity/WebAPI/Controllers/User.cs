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
    public async Task<IActionResult> AddPasswordLogin(string id, [FromBody]AddUserPasswordLogin.Request request)
    {
        return await Mediator.SendThenMap<AddUserPasswordLogin.Request, AddUserPasswordLoginCommand>(request, request => request with { UserId = Hashids.DecodeFirstOrDefault(id) });
    }
}