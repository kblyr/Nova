using System.Text.Json.Serialization;

namespace Nova.Identity.Models;

public record UserEmailAddressVerificationTokenModel
{
    public int UserId { get; init; }
    public string EmailAddress { get; init; } = "";
    public string TokenString { get; init; } = "";
}