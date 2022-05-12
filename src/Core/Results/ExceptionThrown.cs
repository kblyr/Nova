namespace Nova.Results;

public record ExceptionThrownResult(Exception Exception) : IFailedResult;