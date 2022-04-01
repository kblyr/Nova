namespace Nova.HRIS.Schema;

public static class AddProvince
{
    public record Request(string Name);

    public record Response(short Id);
}