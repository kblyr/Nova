namespace Nova.HRIS.Contracts;

public record EmployeeAddedEvent : INotification
{
    public int Id { get; init; }
    public string FullName { get; init; } = "";
    public short EmploymentStatusId { get; init; }
}