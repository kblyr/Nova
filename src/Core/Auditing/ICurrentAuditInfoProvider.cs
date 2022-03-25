namespace Nova.Auditing;

public interface ICurrentAuditInfoProvider
{
    AuditInfo Current { get; }
}
