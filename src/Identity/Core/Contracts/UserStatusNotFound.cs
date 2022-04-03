namespace Nova.Identity.Contracts;

public record UserStatusNotFoundResponse(short Id) : IFailedResponse;