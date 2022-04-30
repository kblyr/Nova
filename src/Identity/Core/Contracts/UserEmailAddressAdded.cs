using MediatR;

namespace Nova.Identity.Contracts;

public record UserEmailAddressAddedEvent
(
    long Id,
    int UserId, 
    string EmailAddress,
    bool IsVerified, 
    bool IsPrimary
) : INotification;