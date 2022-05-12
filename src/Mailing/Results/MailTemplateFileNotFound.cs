namespace Nova.Results;

public record MailTemplateFileNotFoundResult(string FilePath) : IFailedResult;