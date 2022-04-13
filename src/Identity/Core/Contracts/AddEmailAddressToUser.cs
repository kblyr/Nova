namespace Nova.Identity.Contracts;

public record AddEmailAddressToUserCommand(int UserId, string EmailAddress) : IRequest
{
    public record Response(long Id) : IResponse;
}