namespace Nova.Messaging;

public record ApiFailedResponse(ApiFailedResponse.ErrorObj Error)
{
    public record ErrorObj(string Type, object? Data);
}