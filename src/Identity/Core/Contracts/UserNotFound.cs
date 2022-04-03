namespace Nova.Identity.Contracts;

public record UserNotFoundResponse(int Id) : IFailedResponse;