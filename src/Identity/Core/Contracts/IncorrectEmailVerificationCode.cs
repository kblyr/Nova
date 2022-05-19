namespace Nova.Identity.Contracts;

public record IncorrectEmailVerificationCodeResponse : IFailedResponse
{
    public string EmailAddress { get; init; } = "";
    public string VerificationCode { get; init; } = "";
}