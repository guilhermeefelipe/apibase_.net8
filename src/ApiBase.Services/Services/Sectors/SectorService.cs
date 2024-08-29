using ApiBase.Domain.Dto.Sector;
using ApiBase.Repositories.Repositories.Sectors;
using System.ComponentModel.DataAnnotations;

namespace ApiBase.Services.Services.Sectors;

public interface ISectorService
{
    Task<List<SectorDto>> GetListAsync();
    Task<SectorDto?> GetAsync(Guid id);
    Task<Guid> CreateAsync(CreationSectorDto model);
    Task<bool> UpdateAsync(SectorDto model);
    Task<bool> DeleteAsync(Guid id);

}

public class SectorService : ISectorService
{
    private readonly ISectorRepository sectorRepository;


    public SectorService(ISectorRepository sectorRepository)
    {
        this.sectorRepository = sectorRepository;
    }

    public async Task<List<SectorDto>> GetListAsync()
        => await sectorRepository.GetListAsync();

    public async Task<SectorDto?> GetAsync(Guid id)
        => await sectorRepository.GetAsync(id);

    public async Task<bool> DeleteAsync(Guid id)
    => await sectorRepository.DeleteAsync(id);

    public async Task<Guid> CreateAsync(CreationSectorDto model)
    {
        if (model == null) throw new ValidationException("Dados inválidos");

        //VALIDACOES

        var projectId = await sectorRepository.CreateAsync(model);

        return projectId;
    }

    public async Task<bool> UpdateAsync(SectorDto model)
    {
        if (model == null) throw new ValidationException("Dados inválidos");

        //VALIDACOES

        await sectorRepository.UpdateAsync(model);

        return true;
    }
}
