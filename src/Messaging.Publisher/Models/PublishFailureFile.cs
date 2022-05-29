namespace Nova.Models;

public record PublishFailureFileModel
{
    public string MessageType { get; init; } = "";
    public object? Data { get; init; }
    public ErrorObj Error { get; init; } = new();
    
    public record ErrorObj
    {
        public string Type { get; init; } = "";
        public string Message { get; init; } = "";
    }
}