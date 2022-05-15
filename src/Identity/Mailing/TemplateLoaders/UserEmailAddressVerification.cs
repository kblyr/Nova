using Microsoft.Extensions.Options;
using Nova.Identity.Configuration;

namespace Nova.Identity.TemplateLoaders;

sealed class UserEmailAddressVerificationTemplateLoader : MailTemplateLoaderFromFile
{
    public UserEmailAddressVerificationTemplateLoader(IOptions<UserEmailAddressVerificationMailOptions> options) : base(options.Value.TemplatePath)
    {
    }
}