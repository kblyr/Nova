namespace Nova.HRIS.Contracts;

public record EmployeeSalaryGradeStepAddedEvent : INotification
{
    public long Id { get; init; }
    public int EmployeeId { get; init; }
    public short Grade { get; init; }
    public short Step { get; init; }
    public DateTimeOffset EffectivityBeginDate { get; init; }
    public DateTimeOffset? EffectivityEndDate { get; init; }
}