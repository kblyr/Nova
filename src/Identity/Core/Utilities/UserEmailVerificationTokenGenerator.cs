namespace Nova.Identity.Utilities;

public interface IUserEmailVerificationTokenGenerator
{
    string Generate(int userId, string emailAddress);
}