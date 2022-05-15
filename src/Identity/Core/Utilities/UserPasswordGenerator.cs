using Nova.Identity.Configuration;
using Nova.Utilities;

namespace Nova.Identity.Utilities;

public interface IUserPasswordGenerator
{
    string Generate();
}

sealed class UserPasswordGenerator : IUserPasswordGenerator
{
    readonly UserPasswordAutoGenerationOptions _options;
    readonly IRandomStringGenerator _randomStringGenerator;

    public UserPasswordGenerator(IOptions<UserPasswordAutoGenerationOptions> options, IRandomStringGenerator randomStringGenerator)
    {
        _options = options.Value;
        _randomStringGenerator = randomStringGenerator;
    }

    public string Generate()
    {
        return _randomStringGenerator.Generate(_options.Length);
    }
}