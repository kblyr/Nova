using MediatR;

namespace Nova.Identity.Contracts;

public record UserAddedEvent
(
    int Id,
    string Username,
    string EmailAddress,
    short StatusId
) : INotification;