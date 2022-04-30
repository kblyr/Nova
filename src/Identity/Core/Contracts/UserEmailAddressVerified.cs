using MediatR;

namespace Nova.Identity.Contracts;

public record UserEmailAddressVerifiedEvent(int UserId, string EmailAddress) : INotification;