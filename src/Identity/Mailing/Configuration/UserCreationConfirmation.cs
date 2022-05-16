using Nova.Configuration;

namespace Nova.Identity.Configuration;

public record UserCreationConfirmationMailOptions : MailOptions
{
    public const string CONFIGKEY = "Nova:Identity:UserCreationConfirmation:Mail";
}