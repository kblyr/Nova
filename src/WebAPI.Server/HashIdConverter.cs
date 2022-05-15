namespace Nova;

public interface IHashIdConverter<T> 
{
    string Convert(T id);
    T Convert(string hashId);
}