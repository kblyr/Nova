using MediatR;

namespace Nova.Identity.Contracts;

public record AccessTokenGenerated(int UserId, short ApplicationId, AccessTokenGenerated.AccessTokenObj AccessToken) : INotification
{
    public record AccessTokenObj(Guid Id, string TokenString);
}