using Inventory.Application.Common.Pagination;
using Inventory.Application.Dtos.Product;

namespace Inventory.Application.Interfaces.Services
{
    public interface IProductService
    {
        Task<PagedResult<ProductQueryResponse>> SearchAsync(ProductQuery query, CancellationToken cancellationToken);
        Task<ProductDetails> GetDetailsByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<int> CreateAsync(CreateProduct createProduct, CancellationToken cancellationToken);
        Task<int> UpdateAsync(Guid id, UpdateProduct updateProduct, CancellationToken cancellationToken);
        Task<int> DeleteAsync(Guid id, CancellationToken cancellationToken);

    }
}
