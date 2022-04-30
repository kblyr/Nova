using MediatR;

namespace Nova.Identity.Contracts;

public record EmailVerificationSentToUserEvent(int UserId, string EmailAddress) : INotification;