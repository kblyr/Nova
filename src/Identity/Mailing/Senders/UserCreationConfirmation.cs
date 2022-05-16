using Microsoft.Extensions.Options;
using Nova.Configuration;
using Nova.Identity.Configuration;

namespace Nova.Identity.Senders;

sealed class UserCreationConfirmationSender : MailSenderBase
{
    public UserCreationConfirmationSender(IOptions<UserCreationConfirmationMailOptions> options) : base(options.Value)
    {
    }
}
