using System.Runtime.Serialization;

namespace Nova.Exceptions;

public class InaccessibleMailTemplateFileException : Exception
{
    public string TemplatePath { get; }

    public InaccessibleMailTemplateFileException(string templatePath)
    {
        TemplatePath = templatePath;
    }

    public InaccessibleMailTemplateFileException(string templatePath, string? message) : base(message)
    {
        TemplatePath = templatePath;
    }

    public InaccessibleMailTemplateFileException(string templatePath, string? message, Exception? innerException) : base(message, innerException)
    {
        TemplatePath = templatePath;
    }

    protected InaccessibleMailTemplateFileException(string templatePath, SerializationInfo info, StreamingContext context) : base(info, context)
    {
        TemplatePath = templatePath;
    }
}