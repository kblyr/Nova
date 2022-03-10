namespace Nova.Identity.Contracts;

public record AddUser
(
    string Username,
    string Password,
    short StatusId
) : Request
{
    public record Response(int Id) : Messaging.Response;
}