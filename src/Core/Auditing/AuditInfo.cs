namespace Nova.Auditing;

public record AuditInfo(int UserId, DateTimeOffset Timestamp);