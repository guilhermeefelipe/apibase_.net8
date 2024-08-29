using ApiBase.Domain.Dto.Base;
using ApiBase.Domain.Dto.Sector;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiBase.Domain.Dto.Product;

public class ProductDto : BaseDto
{
    public string Name { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime LastUpdateDate { get; set; }
    public Guid SectorId { get; set; }

    [ForeignKey(nameof(SectorId))]
    public SectorDto Sector { get; set; } = default!;

    //public Guid UpdatedById { get; set; }
}
