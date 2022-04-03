namespace Nova.Security;

public interface IStringDecryptor
{
    string Decrypt(string cipher);
}