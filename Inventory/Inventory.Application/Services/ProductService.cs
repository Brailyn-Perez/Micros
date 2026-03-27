using Inventory.Application.Common.Pagination;
using Inventory.Application.Dtos.Product;
using Inventory.Application.Exceptions;
using Inventory.Application.Interfaces.Repositories;
using Inventory.Application.Interfaces.Services;

namespace Inventory.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> CreateAsync(CreateProduct createProduct, CancellationToken cancellationToken)
        {
            var product = new Domain.Entities.Product
            {
                Id = Guid.NewGuid(),
                Name = createProduct.Name,
                Description = createProduct.Description,
                Price = createProduct.Price,
                Stock = createProduct.Stock,
            };

            await _repository.AddAsync(product, cancellationToken);
            return await _repository.SaveChangesAsync(cancellationToken);
        }

        public async Task<int> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var product = await _repository.GetByIdAsync(id, cancellationToken);
            if (product == null)
                throw new BusinessLogicException("Product not found.");

            await _repository.DeleteAsync(id, cancellationToken);
            return await _repository.SaveChangesAsync(cancellationToken);
        }

        public async Task<ProductDetails> GetDetailsByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _repository.GetDetailsByIdAsync(id, cancellationToken);
        }

        public async Task<PagedResult<ProductQueryResponse>> SearchAsync(ProductQuery query, CancellationToken cancellationToken)
        {
            return await _repository.SearchAsync(query, cancellationToken);
        }

        public async Task<int> UpdateAsync(Guid id, UpdateProduct updateProduct, CancellationToken cancellationToken)
        {
            var product = await _repository.GetByIdAsync(id, cancellationToken);
            if (product == null)
                throw new BusinessLogicException("Product not found.");

            product.Name = updateProduct.Name ?? product.Name;
            product.Description = updateProduct.Description ?? product.Description;
            product.Price = updateProduct.Price;
            product.Stock = updateProduct.Stock;

            await _repository.UpdateAsync(product, cancellationToken);
            return await _repository.SaveChangesAsync(cancellationToken);
        }
    }
}
