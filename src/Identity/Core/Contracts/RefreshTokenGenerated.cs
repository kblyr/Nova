using MediatR;

namespace Nova.Identity.Contracts;

public record RefreshTokenGenerated(Guid AccessTokenId, string TokenString) : INotification;