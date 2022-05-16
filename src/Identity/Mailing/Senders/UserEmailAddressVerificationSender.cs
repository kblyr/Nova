using Microsoft.Extensions.Options;
using Nova.Configuration;
using Nova.Identity.Configuration;

namespace Nova.Identity.Senders;

sealed class UserEmailAddressVerificationSender : MailSenderBase
{
    public UserEmailAddressVerificationSender(IOptions<UserEmailAddressVerificationMailOptions> options) : base(options.Value)
    {
    }
}
