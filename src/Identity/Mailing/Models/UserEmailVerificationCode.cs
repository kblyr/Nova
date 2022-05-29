namespace Nova.Identity.Models;

public record UserEmailVerificationCodeModel
{
    public string VerificationCode { get; init; } = "";
}