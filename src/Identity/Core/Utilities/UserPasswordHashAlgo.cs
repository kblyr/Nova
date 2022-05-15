namespace Nova.Identity.Utilities;

public interface IUserPasswordHashAlgo
{
    UserPasswordHashAlgoComputeResult Compute(string password);
    string Compute(string password, string salt);
    bool Verify(string password, string salt);
}

public record struct UserPasswordHashAlgoComputeResult(string HashedPassword, string Salt);

