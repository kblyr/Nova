using System.Text.Json;
using Microsoft.Extensions.Logging;
using Nova.Models;

namespace Nova;

public interface IPublishFailureHandler
{
    Task Handle<T>(T message, Exception exception) where T : class;
}

sealed class LogPublishFailureHandler : IPublishFailureHandler
{
    readonly ILogger<IBusAdapter> _logger;

    public LogPublishFailureHandler(ILogger<IBusAdapter> logger)
    {
        _logger = logger;
    }

    public Task Handle<T>(T message, Exception exception) where T : class
    {
        if (_logger.IsEnabled(LogLevel.Error))
        {
            _logger.LogError("Failed to publish event: {event}", message);
            _logger.LogError("Exception: {exception}", exception);
        }

        return Task.CompletedTask;
    }
}

sealed class SaveToFilePublishFailureHandler : IPublishFailureHandler
{
    string _directory;

    public SaveToFilePublishFailureHandler(string directory)
    {
        _directory = directory;
    }

    public async Task Handle<T>(T message, Exception exception) where T : class
    {
        var messageType = typeof(T).AssemblyQualifiedName;

        if (string.IsNullOrWhiteSpace(messageType))
        {
            return;
        }

        var model = new PublishFailureFileModel
        {
            MessageType = messageType,
            Data = message,
            Error = new()
            {
                Type = exception.GetType().Name,
                Message = exception.Message
            }
        };

        try
        {
            Directory.CreateDirectory(_directory);
            await File.WriteAllTextAsync(Path.Combine(_directory, $"{DateTime.Now:yyyyMMddHHmmss}.json"), JsonSerializer.Serialize(model));
        }
        catch (Exception)
        {
            throw;
        }
    }
}
