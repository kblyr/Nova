namespace Nova.HRIS.Entities;

public class Position
{
    public int Id { get; set; }
    public string Name { get; set; }
    public short Level { get; set; }
    public int? ParentId { get; set; }
    public bool IsDeleted { get; set; }
    public int? InsertedById { get; set; }
    public DateTimeOffset? InsertedOn { get; set; }
    public int? UpdatedById { get; set; }
    public DateTimeOffset? UpdatedOn { get; set; }
    public int? DeletedById { get; set; }
    public DateTimeOffset? DeletedOn { get; set; }

    public Position Parent { get; set; }
    public IEnumerable<Position> Children { get; set; }

    public string FullName { get; set; }
}