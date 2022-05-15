namespace Nova.Identity.Models;

public record UserEmailAddressVerificationModel
{
    public int UserId { get; init; }
    public string EmailAddress { get; init; } = "";
    public string TokenString { get; init; } = "";
}