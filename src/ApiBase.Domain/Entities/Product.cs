using ApiBase.Domain.Dto.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiBase.Domain.Entities;

public class Product : SimpleEntityBase
{
    public string Name { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public DateTime CreationDate { get; set; }
    public DateTime? LastUpdateDate { get; set; }
    public Guid SectorId { get; set; }

    [ForeignKey(nameof(SectorId))]
    public Sector Sector { get; set; } = default!;

    //public Guid UpdatedById { get; set; }
}
