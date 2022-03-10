namespace Nova.Identity.Contracts;

public record GetUserStatuses : Request
{
    public record Response(IEnumerable<Response.UserStatusObj> UserStatuses) : Messaging.Response
    {
        public record UserStatusObj(short Id, string Name);
    }

    public static readonly GetUserStatuses Instance = new();
}