#nullable disable
namespace Nova.HRIS.Entities;

public record SalaryGradeTable
{
    public int Id { get; set; }
    public string Title { get; set; }
    public DateTimeOffset EffectivityBeginDate { get; set; }
    public DateTimeOffset? EffectivityEndDate { get; set; }

    public IEnumerable<SalaryGradeStep> Steps { get; set; }

    public bool IsDeleted { get; set; }
    public int? InsertedById { get; set; }
    public DateTimeOffset? InsertedOn { get; set; }
    public int? DeletedById { get; set; }
    public DateTimeOffset? DeletedOn { get; set; }
}