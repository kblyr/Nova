namespace Nova.Authentication;

public interface ICurrentSessionProvider
{
    Session Current { get; }
}