namespace Nova.Identity.Contracts;

public record UserEmailVerificationCodeAlreadyCreatedResponse : IFailedResponse
{
    public int Id { get; init; }
    public string EmailAddress { get; init; } = "";
}