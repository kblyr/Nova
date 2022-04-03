namespace Nova.Identity.Contracts;

public record AddUserCommand(string Username, short StatusId) : IRequest
{
    public record Response(int Id) : IResponse;
}