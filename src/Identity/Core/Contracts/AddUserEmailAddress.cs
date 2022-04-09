using System.Diagnostics.Contracts;

namespace Nova.Identity.Contracts;

public record AddUserEmailAddressCommand(int UserId, string EmailAddress) : IRequest
{
    public record Response(long Id) : IResponse;
}