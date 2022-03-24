namespace Nova.Identity.Schema;

public static class AddApplicationToUser
{
    public record Request(short ApplicationId);

    public record Response(long Id);
}