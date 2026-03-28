using Inventory.Application.Interfaces.Repositories;
using MassTransit;
using Orders.Api.Messages;

namespace Inventory.Consumer.Consumers
{
    public class OrdersCreatedConsumer : IConsumer<OrderCreated>
    {
        private readonly IProductRepository _repository;
        public async Task Consume(ConsumeContext<OrderCreated> context)
        {
            var product = await _repository.GetByIdAsync(context.Message.ProductId, context.CancellationToken);

            if (product != null && product.Stock >= context.Message.Quantity)
            {
                await _repository.DecreaseStockAsync(context.Message.ProductId, context.Message.Quantity, context.CancellationToken);
            }

            await _repository.SaveChangesAsync(context.CancellationToken);
        }
    }
}
