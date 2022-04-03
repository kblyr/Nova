namespace Nova.Identity.Contracts;

public record AddUserCommand(string Username, string EmailAddress, short StatusId) : IRequest
{
    public record Response(int Id) : IResponse;
}