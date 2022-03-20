namespace Nova.HRIS.Contracts;

public record AddCity(string Name, short? ProvinceId) : Request
{
    public record Response(short Id) : Messaging.Response;
}