namespace Nova.Identity.Contracts;

public record AddUserPasswordLoginCommand(int UserId, string SecurePassword) : IRequest
{
    public record Response(long Id) : IResponse;
}