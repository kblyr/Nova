#nullable disable

namespace Nova.Identity.Entities;

public record UserEmailAddress
{
    public long Id { get; set; }
    public int UserId { get; set; }
    public string EmailAddress { get; set; }
    public short StatusId { get; set; }
    public bool IsPrimary { get; set; }
    
    public bool IsDeleted { get; set; }
    public int? InsertedById { get; set; }
    public DateTimeOffset? InsertedOn { get; set; }
    public int? DeletedById { get; set; }
    public DateTimeOffset? DeletedOn { get; set; }

    public User User { get; set; }
    public UserEmailAddressStatus Status { get; set; }
}