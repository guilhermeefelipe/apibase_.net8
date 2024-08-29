using ApiBase.Domain.Dto.Product;
using ApiBase.Domain.Entities;
using ApiBase.Repositories.DbContexts;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Z.EntityFramework.Plus;

namespace ApiBase.Repositories.Repositories.Products;

public interface IProductRepository
{
    Task<List<ProductDto>> GetListAsync();
    Task<ProductDto?> GetAsync(Guid id);
    Task<Guid> CreateAsync(CreationProductDto data);
    Task UpdateAsync(ProductDto data);
    Task<bool> DeleteAsync(Guid id);
}
public class ProductRepository : IProductRepository
{
    public ApiBaseDbContext dbContext { get; }

    public ProductRepository(ApiBaseDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    public async Task<List<ProductDto>> GetListAsync()
    {
        return await dbContext.Product
            .AsNoTracking()
            .ProjectToType<ProductDto>()
            .ToListAsync();
    }

    public async Task<ProductDto?> GetAsync(Guid id)
    {
        return await dbContext.Product
            .AsNoTracking()
            .ProjectToType<ProductDto>()
            .Where(c => c.Id == id)
            .FirstOrDefaultAsync();
    }

    public async Task<Guid> CreateAsync(CreationProductDto data)
    {
        var newProduct = new Product
        {
            Id = Guid.NewGuid(),
            CreationDate = DateTime.Now,
            Name = data.Name.ToUpper(),
            SectorId = data.SectorId
        };

        await dbContext.Product.AddAsync(newProduct);
        await dbContext.SaveChangesAsync();

        return newProduct.Id;
    }

    public async Task UpdateAsync(ProductDto data)
    => await dbContext.Product
        .AsNoTracking()
        .Where(c => c.Id == data.Id)
        .UpdateAsync(c => new Product
        {
            LastUpdateDate = DateTime.Now,
            Name = data.Name.ToUpper(),
            SectorId = data.SectorId,
            IsActive = data.IsActive,
        });


    public async Task<bool> DeleteAsync(Guid id)
    {
        var product = await dbContext.Product
            .FirstOrDefaultAsync(pu => pu.Id == id);

        if (product == null)
            return false;

        dbContext.Product.Remove(product);
        await dbContext.SaveChangesAsync();

        return true;
    }
}