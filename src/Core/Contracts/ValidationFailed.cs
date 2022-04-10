namespace Nova.Contracts;

public record ValidationFailedResponse(IEnumerable<ValidationFailedResponse.FailureObj> Failures) : IFailedResponse
{
    public record FailureObj(string PropertyName, string ErrorMessage);
}