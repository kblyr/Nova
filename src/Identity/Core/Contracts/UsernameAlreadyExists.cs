namespace Nova.Identity.Contracts;

public record UsernameAlreadyExists(string Username) : FailedResponse;