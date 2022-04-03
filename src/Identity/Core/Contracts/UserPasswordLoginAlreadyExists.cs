namespace Nova.Identity.Contracts;

public record UserPasswordLoginAlreadyExistsResponse(int UserId) : IFailedResponse;