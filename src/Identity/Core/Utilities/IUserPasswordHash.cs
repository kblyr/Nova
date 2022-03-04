namespace Nova.Identity.Utilities;

public interface IUserPasswordHash
{
    string Compute(string password);
}