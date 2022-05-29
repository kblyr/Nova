namespace Nova;

public interface IKeyGenerator { }

public interface IKeyGenerator<TPayload> : IKeyGenerator
{
    RedisKey Generate(TPayload payload);
}