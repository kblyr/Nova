namespace Nova.HRIS.Contracts;

public record AddBarangay(string Name, short? CityId) : Request
{
    public record Response(short Id) : Messaging.Response;
}