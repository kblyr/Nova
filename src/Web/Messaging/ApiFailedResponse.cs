namespace Nova.Messaging;

public record ApiFailedResponse(string ErrorType, object ErrorData);