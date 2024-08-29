using ApiBase.Domain.Dto.Sector;
using ApiBase.Domain.Entities;
using ApiBase.Repositories.DbContexts;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Z.EntityFramework.Plus;

namespace ApiBase.Repositories.Repositories.Sectors;

public interface ISectorRepository
{
    Task<List<SectorDto>> GetListAsync();
    Task<SectorDto?> GetAsync(Guid id);
    Task<Guid> CreateAsync(CreationSectorDto data);
    Task UpdateAsync(SectorDto data);
    Task<bool> DeleteAsync(Guid id);
}
public class SectorRepository : ISectorRepository
{
    public ApiBaseDbContext dbContext { get; }

    public SectorRepository(ApiBaseDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    public async Task<List<SectorDto>> GetListAsync()
    {
        return await dbContext.Sector
            .AsNoTracking()
            .ProjectToType<SectorDto>()
            .ToListAsync();
    }

    public async Task<SectorDto?> GetAsync(Guid id)
    {
        return await dbContext.Sector
            .AsNoTracking()
            .ProjectToType<SectorDto>()
            .Where(c => c.Id == id)
            .FirstOrDefaultAsync();
    }

    public async Task<Guid> CreateAsync(CreationSectorDto data)
    {
        var newSector = new Sector
        {
            Id = Guid.NewGuid(),
            CreationDate = DateTime.Now,
            Name = data.Name.ToUpper(),
            Description = data.Description.ToUpper(),
        };

        await dbContext.Sector.AddAsync(newSector);
        await dbContext.SaveChangesAsync();

        return newSector.Id;
    }

    public async Task UpdateAsync(SectorDto data)
    => await dbContext.Sector
        .AsNoTracking()
        .Where(c => c.Id == data.Id)
        .UpdateAsync(c => new Sector
        {
            LastUpdateDate = DateTime.Now,
            Name = data.Name.ToUpper(),
            IsActive = data.IsActive,
            Description = data.Description.ToUpper(),
        });


    public async Task<bool> DeleteAsync(Guid id)
    {
        var sector = await dbContext.Sector
            .FirstOrDefaultAsync(pu => pu.Id == id);

        if (sector == null)
            return false;

        dbContext.Sector.Remove(sector);
        await dbContext.SaveChangesAsync();

        return true;
    }
}
