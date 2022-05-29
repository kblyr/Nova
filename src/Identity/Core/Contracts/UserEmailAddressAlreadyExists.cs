namespace Nova.Identity.Contracts;

public record UserEmailAddressAlreadyExistsResponse : IFailedResponse
{
    public string EmailAddress { get; init; } = "";
}