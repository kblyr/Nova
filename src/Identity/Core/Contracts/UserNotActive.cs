namespace Nova.Identity.Contracts;

public record UserNotActive(int UserId, short CurrentStatusId) : FailedResponse;