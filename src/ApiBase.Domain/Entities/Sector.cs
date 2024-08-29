using ApiBase.Domain.Dto.Base;

namespace ApiBase.Domain.Entities;

public class Sector : SimpleEntityBase
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public DateTime CreationDate { get; set; }
    public DateTime? LastUpdateDate { get; set; }
}
