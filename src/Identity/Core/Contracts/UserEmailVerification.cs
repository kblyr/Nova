namespace Nova.Identity.Contracts;

public interface IUserEmailVerificationPayload
{
    int UserId { get; init; }
    string EmailAddress { get; init; }
}

public interface IUserEmailVerificationPayloadWithToken : IUserEmailVerificationPayload
{
    string TokenString { get; init; }
}