namespace Nova.HRIS.Entities;

public class SalaryGradeTable
{
    public int Id { get; set; }
    public string Title { get; set; }
    public DateTimeOffset EffectivityDateBegin { get; set; }
    public DateTimeOffset? EffectivityDateEnd { get; set; }
    public bool IsDeleted { get; set; }
    public int? InsertedById { get; set; }
    public DateTimeOffset? InsertedOn { get; set; }
    public int? UpdatedById { get; set; }
    public DateTimeOffset? UpdatedOn { get; set; }
    public int? DeletedById { get; set; }
    public DateTimeOffset? DeletedOn { get; set; }

    public IEnumerable<SalaryGradeStep> Steps { get; set; }
}
