namespace Nova.Identity.Contracts;

public record UserNotLinkedToApplication(int UserId, short ApplicationId) : FailedResponse;