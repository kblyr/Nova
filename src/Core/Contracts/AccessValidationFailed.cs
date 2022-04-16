namespace Nova.Contracts;

public record AccessValidationFailedResponse(IEnumerable<string> ErrorMessages) : IFailedResponse;