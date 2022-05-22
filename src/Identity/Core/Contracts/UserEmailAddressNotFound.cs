namespace Nova.Identity.Contracts;

public record UserEmailAddressNotFoundResponse : IFailedResponse
{
    public int Id { get; init; }
    public string EmailAddress { get; init; } = "";
}