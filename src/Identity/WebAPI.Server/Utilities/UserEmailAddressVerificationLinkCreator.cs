using System.Web;
using Microsoft.Extensions.Options;
using Nova.Identity.Configuration;

namespace Nova.Identity.Utilities;

public interface IUserEmailAddressVerificationLinkCreator
{
    string Create(int userId, string emailAddress, string tokenString);
}

sealed class UserEmailAddressVerificationLinkCreator : IUserEmailAddressVerificationLinkCreator
{
    readonly UserIdConverter _idConverter;
    readonly UserEmailAddressVerificationLinkOptions _options;

    public UserEmailAddressVerificationLinkCreator(UserIdConverter idConverter, IOptions<UserEmailAddressVerificationLinkOptions> options)
    {
        _idConverter = idConverter;
        _options = options.Value;
    }

    public string Create(int userId, string emailAddress, string tokenString)
    {
        return $"{_options.BaseUrl}/{_idConverter.Convert(userId)}/{HttpUtility.UrlEncode(emailAddress)}";
    }
}
