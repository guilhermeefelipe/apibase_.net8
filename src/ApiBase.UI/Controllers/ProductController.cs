using ApiBase.Domain.Dto.Product;
using ApiBase.Services.Services.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[AllowAnonymous]
public class ProductController : ControllerBase
{
    private readonly IProductService productService;

    public ProductController(IProductService productService)
    {
        this.productService = productService;
    }

    [HttpGet("products/")]
    public async Task<ActionResult<ProductDto?>> GetProducts()
    {
        var products = await productService.GetListAsync();

        return Ok(products);
    }

    [HttpPost("products/")]
    public async Task<ActionResult> CreateProduct([FromBody] CreationProductDto productDto)
    {
        if (!ModelState.IsValid) return BadRequest();

        var id = await productService.CreateAsync(productDto);

        return Ok(id);
    }

    [HttpPut("products/")]
    public async Task<ActionResult> UpdateProduct([FromBody] ProductDto productDto)
    {
        if (!ModelState.IsValid) return BadRequest();

        var id = await productService.UpdateAsync(productDto);

        return Ok(id);
    }

    [HttpGet("products/{productId}")]
    public async Task<ActionResult> GetProduct(Guid productId)
    {
        var product = await productService.GetAsync(productId);

        return Ok(product);
    }

    [HttpDelete("products/{productId}")]
    public async Task<ActionResult> DeleteProduct(Guid productId)
    {
        await productService.DeleteAsync(productId);

        return Ok();
    }
}
