namespace Nova.Identity.Contracts;

public record AddPasswordLoginToUserCommand(int UserId, string SecurePassword) : IRequest
{
    public record Response(long Id) : IResponse;
}