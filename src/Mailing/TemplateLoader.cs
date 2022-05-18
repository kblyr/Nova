namespace Nova;

public interface IMailTemplateLoader
{
    ValueTask<Template> Load(CancellationToken cancellationToken = default);
}

public class MailTemplateLoaderFromFile : IMailTemplateLoader
{
    readonly string _filePath;
    Template? _template;

    public MailTemplateLoaderFromFile(string filePath)
    {
        _filePath = filePath;
    }

    public async ValueTask<Template> Load(CancellationToken cancellationToken = default)
    {
        if (_template is not null)
        {
            return _template;
        }

        if (string.IsNullOrWhiteSpace(_filePath) || !File.Exists(_filePath))
        {
            throw new MailTemplateFileNotFoundException(_filePath);
        }

        try
        {
            var templateString = await File.ReadAllTextAsync(_filePath, cancellationToken);
            var template = Template.Parse(templateString);
            _template = template;
            return template;
        }
        catch (UnauthorizedAccessException)
        {
            throw new InaccessibleMailTemplateFileException(_filePath);
        }
        catch (Exception)
        {
            throw;
        }
    }
}
