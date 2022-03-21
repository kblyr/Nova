namespace Nova.Identity.Contracts;

public record GetAccessTokenPayload
(
    int UserId,
    short ApplicationId
) : Request
{
    public record Response
    (
        Response.UserObj User,
        Response.ApplicationObj Application,
        IEnumerable<int> Roles,
        IEnumerable<int> Permissions
    ) : Messaging.Response
    {
        public record UserObj(int Id, string Username, UserStatusObj Status);
        
        public record UserStatusObj(short Id, string Name);

        public record DomainObj(short Id, string Name);

        public record ApplicationObj(short Id, string Name, DomainObj? Domain);
    }
}