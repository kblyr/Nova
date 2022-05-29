namespace Nova.Identity.Contracts;

public record UserEmailVerificationCodeAlreadyCreatedResponse : IFailedResponse
{
    public int UserId { get; init; }
    public string EmailAddress { get; init; } = "";
}