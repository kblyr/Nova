namespace Nova.Senders;

sealed class EmailVerificationCodeSender : MailSenderBase
{
    public EmailVerificationCodeSender(IOptions<EmailVerificationCodeMailOptions> options) : base(options.Value)
    {
    }
}
