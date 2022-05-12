namespace Nova.Results;

public record InaccessibleFileResult(string FilePath) : IFailedResult;
