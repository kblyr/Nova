namespace Nova.Identity.Security;

public interface IUserPasswordHashComputer
{
    string Compute(string password);
}