namespace Nova.Identity.Contracts;

public record UserNotFound(int Id) : FailedResponse;