using Inventory.Api.Entities;
using Inventory.Data;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductFileRepository _repository;

        public ProductController(ProductFileRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetAllProducts()
        {
            var products = _repository.GetAllProducts();
            return Ok(products);
        }

        [HttpGet("{id:guid}")]
        public ActionResult<Product> GetProductById(Guid id)
        {
            var product = _repository.GetProductById(id);

            if (product == null)
                return NotFound();

            return Ok(product);
        }

        [HttpPost]
        public ActionResult<Product> CreateProduct([FromBody] Product newProduct)
        {
            if (_repository.GetProductById(newProduct.Id) != null)
                return Conflict("A product with the same ID already exists.");

            newProduct.Id = Guid.NewGuid();
            newProduct.CreatedDate = DateTimeOffset.UtcNow;

            _repository.AddOrUpdateProduct(newProduct);

            return CreatedAtAction(nameof(GetProductById), new { id = newProduct.Id }, newProduct);
        }

        [HttpPut("{id:guid}")]
        public ActionResult UpdateProduct(Guid id, [FromBody] Product updatedProduct)
        {
            var existingProduct = _repository.GetProductById(id);

            if (existingProduct == null)
                return NotFound();

            updatedProduct.Id = id;
            updatedProduct.CreatedDate = existingProduct.CreatedDate;

            _repository.AddOrUpdateProduct(updatedProduct);

            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public ActionResult DeleteProduct(Guid id)
        {
            var product = _repository.GetProductById(id);

            if (product == null)
                return NotFound();

            _repository.DecreaseStock(id, product.Stock); // Optional: Clear stock before deletion
            _repository.SaveChanges();

            return NoContent();
        }
    }
}