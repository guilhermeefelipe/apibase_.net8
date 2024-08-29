using ApiBase.Domain.Dto.Product;
using ApiBase.Repositories.Repositories.Products;
using System.ComponentModel.DataAnnotations;

namespace ApiBase.Services.Services.Products;

public interface IProductService
{
    Task<List<ProductDto>> GetListAsync();
    Task<ProductDto?> GetAsync(Guid id);
    Task<Guid> CreateAsync(CreationProductDto model);
    Task<bool> UpdateAsync(ProductDto model);
    Task<bool> DeleteAsync(Guid id);

}

public class ProductService : IProductService
{
    private readonly IProductRepository productRepository;


    public ProductService(IProductRepository productRepository)
    {
        this.productRepository = productRepository;
    }

    public async Task<List<ProductDto>> GetListAsync()
        => await productRepository.GetListAsync();

    public async Task<ProductDto?> GetAsync(Guid id)
    => await productRepository.GetAsync(id);

    public async Task<bool> DeleteAsync(Guid id)
    => await productRepository.DeleteAsync(id);

    public async Task<Guid> CreateAsync(CreationProductDto model)
    {
        if (model == null) throw new ValidationException("Dados inválidos");

        //VALIDACOES

        var projectId = await productRepository.CreateAsync(model);

        return projectId;
    }

    public async Task<bool> UpdateAsync(ProductDto model)
    {
        if (model == null) throw new ValidationException("Dados inválidos");

        //VALIDACOES

        await productRepository.UpdateAsync(model);

        return true;
    }
}
