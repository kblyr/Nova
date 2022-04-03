namespace Nova.Security;

public interface ICurrentSessionProvider
{
    Session Current { get; }
}