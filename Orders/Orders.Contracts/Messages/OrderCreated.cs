using System;

namespace Orders.Api.Messages
{
    public class OrderCreated
    {
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
