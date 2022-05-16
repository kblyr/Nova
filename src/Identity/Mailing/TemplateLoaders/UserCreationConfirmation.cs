using Microsoft.Extensions.Options;
using Nova.Identity.Configuration;

namespace Nova.Identity.TemplateLoaders;

sealed class UserCreationConfirmationTemplateLoader : MailTemplateLoaderFromFile
{
    public UserCreationConfirmationTemplateLoader(IOptions<UserCreationConfirmationMailOptions> options) : base(options.Value.TemplatePath)
    {
    }
}
