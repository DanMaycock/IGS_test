using IgsTest.Models;
using Microsoft.EntityFrameworkCore;

namespace IgsTest.Services;

public class ProductsService
{
  private readonly ProductsContext _dbContext;

  public ProductsService(ProductsContext dbContext)
  {
    _dbContext = dbContext;
  }

  public async Task<List<Product>> Get()
  {
    return await _dbContext.Products.AsQueryable().ToListAsync();
  }

  public async Task<Product?> Get(string id) => await _dbContext.Products.FindAsync(Int32.Parse(id));

  public async Task Create(Product newProduct)
  {
    _dbContext.Add(newProduct);
    await _dbContext.SaveChangesAsync();
  }

  public async Task Update(string id, Product updatedProduct)
  {
    var product = await _dbContext.Products.FindAsync(Int32.Parse(id));
    if (product != null)
    {
      product.Name = updatedProduct.Name;
      product.Price = updatedProduct.Price;

      await _dbContext.SaveChangesAsync();
    }
  }
  public async Task Remove(string id)
  {
    var product = await _dbContext.Products.FindAsync(Int32.Parse(id));
    if (product != null)
    {
      _dbContext.Products.Remove(product);
      await _dbContext.SaveChangesAsync();
    }
  }
}