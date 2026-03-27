using Inventory.Application.Dtos.Product;
using Inventory.Domain.Entities;
using System.Linq.Expressions;

namespace Inventory.infrastructure.Projections.Product
{
    public static class ProductProjections
    {
        public static Expression<Func<Domain.Entities.Product, ProductQueryResponse>> Search => p => new ProductQueryResponse
        {
            Id = p.Id,
            Name = p.Name,
            Description = p.Description,
            Price = p.Price,
            Stock = p.Stock,
        };

        public static Expression<Func<Domain.Entities.Product, ProductDetails>> Details => p => new ProductDetails
        {
            Id = p.Id,
            Name = p.Name,
            Description = p.Description,
            Price = p.Price,
            Stock = p.Stock,
        };
    }
}
