namespace Nova.HRIS.Contracts;

public record EmploymentAddedEvent : INotification
{
    public long Id { get; init; }
    public int EmployeeId { get; init; }
    public short TypeId { get; init; }
    public int OfficeId { get; init; }
    public int PositionId { get; init; }
    public decimal? Salary { get; init; }
    public DateTimeOffset EffectivityBeginDate { get; init; }
    public DateTimeOffset? EffectivityEndDate { get; init; }
}