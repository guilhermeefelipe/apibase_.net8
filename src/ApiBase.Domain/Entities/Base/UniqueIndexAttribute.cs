namespace ApiBase.Domain.Entities.Base;

[AttributeUsage(AttributeTargets.All)]
public sealed class UniqueIndexAttribute : Attribute
{
    public string[] Fields { get; }

    public UniqueIndexAttribute(params string[] fields)
        => Fields = fields;
}
