#nullable disable

namespace Nova.Identity.Entities;

public class UserEmailAddress
{
    public long Id { get; set; }
    public int UserId { get; set; }
    public string EmailAddress { get; set; }
    public bool IsVerified { get; set; }
    public bool IsPrimary { get; set; }

    public bool IsDeleted { get; set; }
    public int? InsertedById { get; set; }
    public DateTimeOffset? InsertedOn { get; set; }
    public int? UpdatedById { get; set; }
    public DateTimeOffset? UpdatedOn { get; set; }
    public int? DeletedById { get; set; }
    public DateTimeOffset? DeletedOn { get; set; }

    public User User { get; set; }
}