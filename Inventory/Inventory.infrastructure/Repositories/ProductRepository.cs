using Inventory.Application.Common.Pagination;
using Inventory.Application.Dtos.Product;
using Inventory.Application.Interfaces.Repositories;
using Inventory.Domain.Entities;
using Inventory.infrastructure.Extensions;
using Inventory.infrastructure.Projections.Product;
using Inventory.infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Inventory.infrastructure.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly InventoryDbContext _context;
        public ProductRepository(InventoryDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task DecreaseStockAsync(Guid Id, int quantity, CancellationToken cancellationToken)
        {
            var product = await _context.Products.FindAsync(new object[] { Id }, cancellationToken);
            if (product != null && product.Stock >= quantity)
            {
                product.Stock -= quantity;
                _context.Products.Update(product);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task<PagedResult<ProductQueryResponse>> SearchAsync(ProductQuery query, CancellationToken cancellationToken)
        {
            var Queryable = _context.Products.AsNoTracking().AsQueryable();

            if (!string.IsNullOrEmpty(query.Name))
            {
                Queryable = Queryable.Where(p => p.Name.Contains(query.Name));
            }
            if (query.MinStock.HasValue)
            {
                Queryable = Queryable.Where(p => p.Stock >= query.MinStock.Value);
            }
            if (query.MaxStock.HasValue)
            {
                Queryable = Queryable.Where(p => p.Stock <= query.MaxStock.Value);
            }

            if (!query.IncludeOutOfStock)
            {
                Queryable = Queryable.Where(p => p.Stock > 0);
            }

            return await Queryable
                        .Select(ProductProjections.Search)
                        .PaginateAsync(query.PageNumber, query.PageSize, cancellationToken);

        }
    }
}
