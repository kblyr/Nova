using Nova.Identity.Configuration;
using Nova.Utilities;

namespace Nova.Identity.Utilities;

public interface IUserEmailAddressVerificationTokenGenerator 
{
    string Generate();
}

sealed class UserEmailAddressVerificationTokenGenerator : IUserEmailAddressVerificationTokenGenerator
{
    readonly IRandomStringGenerator _randomStringGenerator;
    readonly UserEmailAddressVerificationTokenOptions _options;

    public UserEmailAddressVerificationTokenGenerator(IRandomStringGenerator randomStringGenerator, IOptions<UserEmailAddressVerificationTokenOptions> options)
    {
        _randomStringGenerator = randomStringGenerator;
        _options = options.Value;
    }

    public string Generate()
    {
        return _randomStringGenerator.Generate(_options.Length);
    }
}