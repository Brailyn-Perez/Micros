namespace Inventory.Api.Entities
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Stock { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
    }
}
