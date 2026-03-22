using Inventory.Data;
using MassTransit;
using Orders.Api.Messages;

namespace Inventory.Consumer.Consumers
{
    public class OrdersCreatedConsumer : IConsumer<OrderCreated>
    {
        private readonly ProductFileRepository _repo;

        public OrdersCreatedConsumer()
        {
            _repo = new ProductFileRepository();
        }

        public Task Consume(ConsumeContext<OrderCreated> context)
        {
            var product = _repo.GetProductById(context.Message.ProductId);

            if (product != null && product.Stock >= context.Message.Quantity)
            {
                _repo.DecreaseStock(context.Message.ProductId, context.Message.Quantity);
            }

            _repo.SaveChanges();

            return Task.CompletedTask;
        }
    }
}
