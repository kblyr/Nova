namespace Nova.KeyGenerators;

public interface IKeyGenerator<TPayload>
{
    RedisKey Generate(TPayload payload);
}