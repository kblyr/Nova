namespace Nova.Identity.TemplateLoaders;

public sealed class UserEmailVerificationCodeTemplateLoader : MailTemplateLoaderFromFile
{
    public UserEmailVerificationCodeTemplateLoader(IOptions<UserEmailVerificationCodeMailOptions> options) : base(options.Value.TemplatePath)
    {
    }
}
