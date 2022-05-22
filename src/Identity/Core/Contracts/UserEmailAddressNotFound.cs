namespace Nova.Identity.Contracts;

public record UserEmailAddressNotFoundResponse : IFailedResponse
{
    public int UserId { get; init; }
    public string EmailAddress { get; init; } = "";
}