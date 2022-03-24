namespace Nova.Contracts;

public record ValidationFailed(IEnumerable<ValidationFailed.FailureObj> Failures) : FailedResponse
{
    public record FailureObj(string PropertyName, string ErrorMessage);
}