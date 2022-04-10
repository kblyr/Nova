namespace Nova.Configuration;

public record StringPropertyConfig
{
    public int MinLength { get; init; }
    public int MaxLength { get; init; }
}