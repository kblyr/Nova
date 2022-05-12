namespace Nova.Identity.Models;

record UserEmailVerificationToken
{
    public int UserId { get; init; }
    public string EmailAddress { get; init; } = "";
    public string TokenString { get; init; } = "";
    public DateTimeOffset ResendOn { get; init; }
}