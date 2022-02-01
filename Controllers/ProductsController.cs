using Microsoft.AspNetCore.Mvc;
using IgsTest.Models;
using IgsTest.Services;

namespace IgsTest.Controllers;

[ApiController]
[Route("v1/")]
public class ProductController : ControllerBase
{
  private readonly ProductsService _productService;

  public ProductController(ProductsService productService)
  {
    _productService = productService;
  }

  [HttpGet("products")]
  public async Task<List<Product>> Get() =>
      await _productService.Get();

  [HttpGet("product/{id}")]
  public async Task<ActionResult<Product>> Get(string id)
  {
    var product = await _productService.Get(id);

    if (product is null)
    {
      return NotFound();
    }
    return product;
  }

  [HttpPost("product")]
  public async Task<IActionResult> Post([FromForm] Product newProduct)
  {
    await _productService.Create(newProduct);

    return new OkObjectResult(newProduct);
  }

  [HttpPut("product/{id}")]
  public async Task<IActionResult> Update(string id, [FromForm] Product updatedProduct)
  {
    var product = await _productService.Get(id);
    if (product is null)
    {
      return NotFound();
    }

    updatedProduct.Id = product.Id;
    await _productService.Update(id, updatedProduct);

    return new EmptyResult();
  }

  [HttpDelete("product/{id}")]
  public async Task<IActionResult> Delete(string id)
  {
    var product = await _productService.Get(id);

    if (product is null)
    {
      return NotFound();
    }

    await _productService.Remove(id);

    return new EmptyResult();
  }

}
