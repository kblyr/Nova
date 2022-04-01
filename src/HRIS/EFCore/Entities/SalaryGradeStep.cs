namespace Nova.HRIS.Entities;

public class SalaryGradeStep
{
    public long Id { get; set; }
    public int TableId { get; set; }
    public short Grade { get; set; }
    public short Step { get; set; }
    public decimal Amount { get; set; }
    public bool IsDeleted { get; set; }
    public int? InsertedById { get; set; }
    public DateTimeOffset? InsertedOn { get; set; }
    public int? UpdatedById { get; set; }
    public DateTimeOffset? UpdatedOn { get; set; }
    public int? DeletedById { get; set; }
    public DateTimeOffset? DeletedOn { get; set; }

    public SalaryGradeTable Table { get; set; }
}