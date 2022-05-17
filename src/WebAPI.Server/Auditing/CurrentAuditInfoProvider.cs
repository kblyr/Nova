namespace Nova.Auditing;

sealed class CurrentAuditInfoProvider : ICurrentAuditInfoProvider
{
    public AuditInfo Current => throw new NotImplementedException();
}
