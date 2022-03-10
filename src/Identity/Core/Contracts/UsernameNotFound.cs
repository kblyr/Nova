namespace Nova.Identity.Contracts;

public record UsernameNotFound(string username) : FailedResponse;