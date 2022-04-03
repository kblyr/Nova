namespace Nova.Identity.Contracts;

public record UserEmailAddressAlreadyExistsResponse(string EmailAddress) : IFailedResponse;