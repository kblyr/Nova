namespace Nova.Identity.Configuration;

public record UserPasswordAutoGenerationOptions
{
    public const string CONFIGKEY = "Nova:Identity:UserPasswordAutoGeneration";

    public int Length { get; init; } = 7;
}