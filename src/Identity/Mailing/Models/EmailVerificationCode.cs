namespace Nova.Models;

public record EmailVerificationCodeModel
{
    public string VerificationCode { get; init; } = "";
}