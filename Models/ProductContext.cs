using Microsoft.EntityFrameworkCore;

namespace IgsTest.Models
{
  public class ProductsContext : DbContext
  {
    public DbSet<Product> Products { get; set; } = null!;

    public string DbPath { get; }

    public ProductsContext()
    {
      var folder = Environment.SpecialFolder.LocalApplicationData;
      var path = Environment.GetFolderPath(folder);
      DbPath = System.IO.Path.Join(path, "product.db");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
      => options.UseSqlite($"Data Source={DbPath}");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Product>().HasData(
        new Product { Id = 1, Name = "Lavender heart", Price = "9.25" },
        new Product { Id = 2, Name = "Personalised cufflinks", Price = "45.00" },
        new Product { Id = 3, Name = "Kids T-shirt", Price = "19.95" }
      );
    }
  }

  public class Product
  {
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Price { get; set; }
  }
}

