namespace Nova.Identity.KeyGenerators;

public sealed class UserEmailVerificationCodeKeyGenerator : IKeyGenerator<UserEmailVerificationCodeKeyGenerator.Payload>
{
    const string FORMAT = "nova/identity/user-email-verification-code/{0}/{1}";

    public RedisKey Generate(Payload payload)
    {
        return new RedisKey(string.Format(FORMAT, payload.Id, payload.EmailAddress));
    }

    public record Payload
    {
        public int Id { get; init; }
        public string EmailAddress { get; init; } = "";
    }
}