namespace Nova.Identity.Contracts;

public record IncorrectUserEmailVerificationCodeResponse : IFailedResponse
{
    public int UserId { get; init; }
    public string EmailAddress { get; init; } = "";
    public string VerificationCode { get; init; } = "";
}