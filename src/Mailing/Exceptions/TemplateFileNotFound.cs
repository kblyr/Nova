using System.Runtime.Serialization;

namespace Nova.Exceptions;

public class MailTemplateFileNotFoundException : Exception
{
    public string TemplatePath { get; }

    public MailTemplateFileNotFoundException(string templatePath)
    {
        TemplatePath = templatePath;
    }

    public MailTemplateFileNotFoundException(string templatePath, string? message) : base(message)
    {
        TemplatePath = templatePath;
    }

    public MailTemplateFileNotFoundException(string templatePath, string? message, Exception? innerException) : base(message, innerException)
    {
        TemplatePath = templatePath;
    }

    protected MailTemplateFileNotFoundException(string templatePath, SerializationInfo info, StreamingContext context) : base(info, context)
    {
        TemplatePath = templatePath;
    }
}
