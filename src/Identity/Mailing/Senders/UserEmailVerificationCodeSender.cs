namespace Nova.Identity.Senders;

public sealed class UserEmailVerificationCodeSender : MailSenderBase, IMailSender
{
    public UserEmailVerificationCodeSender(IOptions<UserEmailVerificationCodeMailOptions> options) : base(options.Value)
    {
    }
}
