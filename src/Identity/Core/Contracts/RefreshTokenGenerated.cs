using MediatR;

namespace Nova.Identity.Contracts;

public record RefreshTokenGenerated(string AccessTokenId, string TokenString) : INotification;