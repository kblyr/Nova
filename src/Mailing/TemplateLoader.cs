namespace Nova;

public interface IMailTemplateLoader<TPayload>
{
    ValueTask<IResult> Load();
}

abstract class MailTemplateLoader<TPayload> : IMailTemplateLoader<TPayload>
{
    readonly MailTemplateCache _cache;
    readonly MailTemplateLoaderOptions<TPayload>? _options;

    public MailTemplateLoader(MailTemplateCache cache, MailTemplateLoaderOptions<TPayload>? options)
    {
        _cache = cache;
        _options = options;
    }

    protected abstract Task<IResult> LoadTemplate();

    public async ValueTask<IResult> Load()
    {
        var tPayload = typeof(TPayload);
        var isCached = _options?.IsCached ?? MailTemplateLoaderOptions.Default.IsCached;

        if (isCached)
        {
            var cachedTemplate = _cache.Get(tPayload);

            if (cachedTemplate is not null)
                return new MailTemplateLoadedResult(cachedTemplate);
        }

        var loadResult = await Load();

        if (loadResult is MailTemplateLoadedResult result && isCached)
            _cache.Set(tPayload, result.Template);

        return loadResult;
    }
}

sealed class MailTemplateLoaderFromFile<TPayload> : MailTemplateLoader<TPayload>
{
    readonly string _filePath;

    public MailTemplateLoaderFromFile(MailTemplateCache cache, string _filePath, MailTemplateLoaderOptions<TPayload>? options) : base(cache, options)
    {
        this._filePath = _filePath;
    }

    protected override async Task<IResult> LoadTemplate()
    {
        if (!File.Exists(_filePath))
            return new MailTemplateFileNotFoundResult(_filePath);

        try
        {
            var templateString = await File.ReadAllTextAsync(_filePath);
            var template = Template.Parse(templateString, _filePath);
            return new MailTemplateLoadedResult(template);
        }
        catch (UnauthorizedAccessException)
        {
            return new InaccessibleFileResult(_filePath);
        }
        catch (Exception)
        {
            throw;
        }
    }
}

public record MailTemplateLoaderOptions(bool IsCached) 
{
    public static readonly MailTemplateLoaderOptions Default = new(true);
}

public record MailTemplateLoaderOptions<TPayload> : MailTemplateLoaderOptions
{
    public MailTemplateLoaderOptions(bool IsCached) : base(IsCached)
    {
    }
}

sealed class MailTemplateCache
{
    readonly Dictionary<Type, Template> _templates = new();

    public Template? Get(Type tPayload)
    {
        if (_templates.ContainsKey(tPayload))
            return _templates[tPayload];

        return null;
    }

    public Template? Get<TPayload>()
    {
        return Get(typeof(TPayload));
    }

    public void Set(Type tPayload, Template template, bool isForced = true)
    {
        if (tPayload is null || template is null)
            return;

        if (!_templates.ContainsKey(tPayload)) 
            _templates.Add(tPayload, template);
        else if (isForced)
            _templates[tPayload] = template;
    }

    public void Set<TPayload>(Template template, bool isForced = true)
    {
        Set(typeof(TPayload), template, isForced);
    }
}