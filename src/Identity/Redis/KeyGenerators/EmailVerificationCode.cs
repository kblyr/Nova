namespace Nova.KeyGenerators;

public sealed class EmailVerificationCodeKeyGenerator : IKeyGenerator<EmailVerificationCodeKeyGenerator.Payload>
{
    const string FORMAT = "nova/identity/email-verification-code/{0}";

    public RedisKey Generate(Payload payload)
    {
        return new RedisKey(string.Format(FORMAT, payload.EmailAddress));
    }

    public record Payload
    {
        public string EmailAddress { get; init; } = "";
    }
}
