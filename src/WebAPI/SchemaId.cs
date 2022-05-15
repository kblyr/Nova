namespace Nova;

[AttributeUsage(AttributeTargets.Class)]
public class SchemaIdAttribute : Attribute
{
    public string SchemaId { get; }

    public SchemaIdAttribute(string schemaId)
    {
        SchemaId = schemaId;
    }
}