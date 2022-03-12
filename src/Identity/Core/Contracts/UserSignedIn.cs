using MediatR;

namespace Nova.Identity.Contracts;

public record UserSignedIn(int UserId, short ApplicationId) : INotification;