namespace Nova.Identity.Models;

public record UserEmailVerificationCodeModel
{
    public int UserId { get; init; }
    public string EmailAddress { get; init; } = "";
    public string VerificationCode { get; init; } = "";
}