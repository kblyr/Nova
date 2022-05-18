namespace Nova.Security;

public interface IUserPasswordHashAlgorithm
{
    UserPasswordHashAlgorithmComputeResult Compute(string password);
    string Compute(string password, string salt);
    bool Verify(string password, string salt);
}

public record struct UserPasswordHashAlgorithmComputeResult(string HashedPassword, string Salt);