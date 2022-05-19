namespace Nova.Identity.Models;

public record EmailVerificationCodeModel
{
    public string EmailAddress { get; init; } = "";
    public string VerificationCode { get; init; } = "";
}