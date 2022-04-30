using MediatR;

namespace Nova.Identity.Contracts;

public record UserEmailVerificationSendRequestedEvent(int UserId, string EmailAddress) : INotification;