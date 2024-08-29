using System.ComponentModel.DataAnnotations;

namespace ApiBase.Domain.Dto.Base;

public abstract class SimpleEntityBase
{
    /// <summary>
    /// Chave primária padrão para todas as tabelas que requerem Guid
    /// </summary>
    [Key]
    public Guid Id { get; set; }
}