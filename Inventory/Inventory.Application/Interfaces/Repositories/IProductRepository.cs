using Inventory.Application.Common.Pagination;
using Inventory.Application.Dtos.Product;
using Inventory.Domain.Entities;
using Inventory.Domain.Repository;

namespace Inventory.Application.Interfaces.Repositories
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<PagedResult<ProductQueryResponse>> SearchAsync(ProductQuery query, CancellationToken cancellationToken);
        Task DecreaseStockAsync(Guid productId, int quantity, CancellationToken cancellationToken);
    }
}
