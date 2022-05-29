namespace Nova.Auditing;

public record struct AuditInfo(int? UserId, DateTimeOffset? Timestamp);