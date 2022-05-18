namespace Nova.TemplateLoaders;

sealed class EmailVerificationCodeTemplateLoader : MailTemplateLoaderFromFile
{
    public EmailVerificationCodeTemplateLoader(IOptions<EmailVerificationCodeMailOptions> options) : base(options.Value.TemplatePath)
    {
    }
}
