namespace Nova.Identity.KeyGenerators;

sealed class UserEmailAddressVerificationTokenKeyGenerator : IKeyGenerator<UserEmailAddressVerificationTokenModel>
{
    const string FORMAT = "nova/identity/user-email-address-verification-token/{0}/{1}";

    public RedisKey Generate(UserEmailAddressVerificationTokenModel model)
    {
        return new RedisKey(string.Format(FORMAT, model.UserId, model.EmailAddress));
    }
}
