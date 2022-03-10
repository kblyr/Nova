namespace Nova.Identity.Entities;

public class UserApplication
{
    public long Id { get; set; }
    public int UserId { get; set; }
    public short ApplicationId { get; set; }
    public bool IsDeleted { get; set; }
    public int? InsertedById { get; set; }
    public DateTimeOffset? InsertedOn { get; set; }
    public int? DeletedById { get; set; }
    public DateTimeOffset? DeletedOn { get; set; }

    public User User { get; set; }
    public Application Application { get; set; }
}