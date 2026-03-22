using Inventory.Api.Entities;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Inventory.Data
{
    public class InventoryDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Server=localhost,1433;Database=InventoryDb;User Id=sa;Password=YourStrong!Pass123;TrustServerCertificate=True;"
            );
        }

        public void SeedDatabase(string jsonFilePath)
        {
            Database.EnsureCreated();

            if (!File.Exists(jsonFilePath)) return;

            var jsonData = File.ReadAllText(jsonFilePath);
            var products = JsonSerializer.Deserialize<List<Product>>(jsonData);

            if (products != null && !Products.Any())
            {
                Products.AddRange(products);
                SaveChanges();
            }
        }
    }
}