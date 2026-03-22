using MassTransit;
using Orders.Api.Messages;

namespace Orders.Api.Producer
{
    public class OrdersProducer
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public OrdersProducer(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        public async Task PublishOrderCreatedAsync(OrderCreated orderCreated)
        {
            var message = new OrderCreated
            {
                OrderId = orderCreated.OrderId,
                ProductId = orderCreated.ProductId,
                Quantity = orderCreated.Quantity
            };
            await _publishEndpoint.Publish(message);
        }
    }
}
