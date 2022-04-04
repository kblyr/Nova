namespace Nova.Identity.Contracts;

public record UserEmailAddressAlreadyExistsResponse(int UserId, string EmailAddress) : IFailedResponse;