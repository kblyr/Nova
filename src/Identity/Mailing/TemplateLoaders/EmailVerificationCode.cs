namespace Nova.Identity.TemplateLoaders;

public sealed class EmailVerificationCodeTemplateLoader : MailTemplateLoaderFromFile
{
    public EmailVerificationCodeTemplateLoader(IOptions<EmailVerificationCodeMailOptions> options) : base(options.Value.TemplatePath)
    {
    }
}
