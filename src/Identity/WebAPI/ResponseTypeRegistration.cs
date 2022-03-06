namespace Nova.Identity;

sealed class ResponseTypeRegistration : IResponseTypeRegistration
{
    public void Register(ResponseTypeRegistry registry)
    {
        registry
            .Register<Contracts.AddUser.Response, AddUser.Response>(StatusCodes.Status201Created);
    }
}
