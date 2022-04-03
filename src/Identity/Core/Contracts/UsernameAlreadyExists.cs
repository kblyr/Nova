namespace Nova.Identity.Contracts;

public record UsernameAlreadyExistsResponse(string Username) : IFailedResponse;