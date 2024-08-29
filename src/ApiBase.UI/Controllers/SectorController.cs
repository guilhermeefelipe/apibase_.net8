using ApiBase.Domain.Dto.Sector;
using ApiBase.Services.Services.Sectors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiBase.UI.Controllers;

[ApiController]
[AllowAnonymous]
public class SectorController : ControllerBase
{
    private readonly ISectorService sectorService;

    public SectorController(ISectorService sectorService)
    {
        this.sectorService = sectorService;
    }

    [HttpGet("sectors/")]
    public async Task<ActionResult<SectorDto?>> GetSectors()
    {
        var Sectors = await sectorService.GetListAsync();

        return Ok(Sectors);
    }

    [HttpPost("sectors/")]
    public async Task<ActionResult> CreateSector([FromBody] CreationSectorDto SectorDto)
    {
        if (!ModelState.IsValid) return BadRequest();

        var id = await sectorService.CreateAsync(SectorDto);

        return Ok(id);
    }

    [HttpPut("sectors/")]
    public async Task<ActionResult> UpdateSector([FromBody] SectorDto SectorDto)
    {
        if (!ModelState.IsValid) return BadRequest();

        var id = await sectorService.UpdateAsync(SectorDto);

        return Ok(id);
    }

    [HttpGet("sectors/{sectorId}")]
    public async Task<ActionResult> GetSector(Guid sectorId)
    {
        var sector = await sectorService.GetAsync(sectorId);

        return Ok(sector);
    }

    [HttpDelete("sectors/{sectorId}")]
    public async Task<ActionResult> DeleteSector(Guid sectorId)
    {
        await sectorService.DeleteAsync(sectorId);

        return Ok();
    }
}
