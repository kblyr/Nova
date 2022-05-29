namespace Nova.HRIS.Contracts;

public record InvalidSalaryGradeStepResponse : IFailedResponse
{
    public short Grade { get; init; }
    public short Step { get; init; }
    public DateTimeOffset EffectivityBeginDate { get; init; }
    public DateTimeOffset? EffectivityEndDate { get; init; }
}