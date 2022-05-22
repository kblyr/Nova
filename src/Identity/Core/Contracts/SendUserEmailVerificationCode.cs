namespace Nova.Identity.Contracts;

public record SendUserEmailVerificationCodeCommand : IRequest
{
    public int Id { get; init; }
    public string EmailAddress { get; init; } = "";
    public string VerificationCode { get; init; } = "";

    public record Response : IResponse
    {
        public static readonly Response Instance = new();
    }
}

public record SendUserEmailVerificationCodeRequestedEvent : INotification
{
    public int Id { get; init; }
    public string EmailAddress { get; init; } = "";
    public string VerificationCode { get; init; } = "";
}