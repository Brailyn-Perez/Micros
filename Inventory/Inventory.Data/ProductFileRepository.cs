using Inventory.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Data
{
    public class ProductFileRepository
    {
        private readonly InventoryDbContext _context;

        public ProductFileRepository()
        {
            _context = new InventoryDbContext();
            _context.SeedDatabase(Path.Combine(AppContext.BaseDirectory, "Data", "Stock.json"));
        }

        public List<Product> GetAllProducts()
        {
            return _context.Products.ToList();
        }

        public Product? GetProductById(Guid productId)
        {
            return _context.Products.FirstOrDefault(p => p.Id == productId);
        }

        public void AddOrUpdateProduct(Product product)
        {
            var existingProduct = _context.Products.FirstOrDefault(p => p.Id == product.Id);

            if (existingProduct != null)
            {
                existingProduct.Name = product.Name;
                existingProduct.Stock = product.Stock;
                existingProduct.CreatedDate = product.CreatedDate;
            }
            else
            {
                _context.Products.Add(product);
            }

            _context.SaveChanges();
        }

        public void DecreaseStock(Guid productId, int quantity)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == productId);

            if (product == null) return;

            product.Stock -= quantity;
            _context.Products.Update(product);
            _context.SaveChanges();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}