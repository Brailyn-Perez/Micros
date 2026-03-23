using Inventory.Application.Common.Pagination;

namespace Inventory.Application.Dtos.Product
{
    public class ProductQuery : PaginationFilter
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int? MinStock { get; set; }
        public int? MaxStock { get; set; }
        public bool IncludeOutOfStock { get; set; }
    }
}
