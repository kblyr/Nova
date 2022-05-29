namespace Nova.Identity.Contracts;

public record EmailVerificationCodeAlreadyCreatedResponse : IFailedResponse
{
    public string EmailAddress { get; init; } = "";
} 