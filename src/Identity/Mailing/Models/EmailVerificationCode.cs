namespace Nova.Identity.Models;

public record EmailVerificationCodeModel
{
    public string VerificationCode { get; init; } = "";
}