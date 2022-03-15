namespace Nova.Identity.Contracts;

public record UserApplicationAlreadyExists(int UserId, short ApplicationId) : FailedResponse;