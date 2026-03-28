namespace Orders.Api.Entities
{
    public class Order
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
