namespace Nova.Identity.KeyGenerators;

sealed class UserEmailVerificationKeyGenerator : IKeyGenerator<IUserEmailVerificationPayload>
{
    public RedisKey Generate(IUserEmailVerificationPayload payload)
    {
        return $"Nova/Identity/UserEmailVerificationToken/{payload.UserId}/{payload.EmailAddress}";
    }
}
