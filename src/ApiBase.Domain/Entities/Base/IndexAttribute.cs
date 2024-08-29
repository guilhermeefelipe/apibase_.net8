namespace ApiBase.Domain.Entities.Base;

/// <summary>
/// Atributo para identificar índices compostos a serem
/// criados no banco de dados
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public sealed class IndexAttribute : Attribute
{
    public string[] Fields { get; }

    public IndexAttribute(params string[] fields)
        => Fields = fields;
}