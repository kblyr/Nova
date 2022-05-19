namespace Nova.Identity.Senders;

public sealed class EmailVerificationCodeSender : MailSenderBase, IMailSender
{
    public EmailVerificationCodeSender(IOptions<EmailVerificationCodeMailOptions> options) : base(options.Value)
    {
    }
}
