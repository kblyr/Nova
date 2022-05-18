namespace Nova.Contracts;

public record EmailVerificationCodeAlreadyCreatedResponse : IFailedResponse
{
    public string EmailAddress { get; init; } = "";
} 