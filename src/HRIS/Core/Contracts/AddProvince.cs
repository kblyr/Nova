namespace Nova.HRIS.Contracts;

public record AddProvince(string Name) : Request
{
    public record Response(short Id) : Messaging.Response;
}