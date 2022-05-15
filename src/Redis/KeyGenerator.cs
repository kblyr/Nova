namespace Nova;

public interface IKeyGenerator<TModel>
{
    RedisKey Generate(TModel model);
}