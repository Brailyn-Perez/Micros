using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Orders.Api.Entities;
using Orders.Api.Producer;
using System.Collections.Generic;
using System.Linq;

namespace Orders.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {

        private static readonly List<Order> Orders = new();
        private readonly Producer.OrdersProducer _ordersProducer;

        public OrderController(OrdersProducer ordersProducer)
        {
            _ordersProducer = ordersProducer;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Order>> GetOrders()
        {
            return Ok(Orders);
        }

        [HttpGet("{id:guid}")]
        public ActionResult<Order> GetOrder(Guid id)
        {
            var order = Orders.FirstOrDefault(o => o.Id == id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder([FromBody] Order order)
        {
            if (order == null)
            {
                return BadRequest("Order cannot be null.");
            }

            order.Id = Guid.NewGuid();
            order.CreatedAt = DateTime.UtcNow;
            Orders.Add(order);

            var orderCreated = new Messages.OrderCreated
            {
                OrderId = order.Id,
                ProductId = order.ProductId,
                Quantity = order.Quantity
            };

            await _ordersProducer.PublishOrderCreatedAsync(orderCreated);

            return CreatedAtAction(nameof(GetOrder), new { id = order.Id }, order);
        }

        [HttpPut("{id:guid}")]
        public ActionResult UpdateOrder(Guid id, [FromBody] Order updatedOrder)
        {
            var existingOrder = Orders.FirstOrDefault(o => o.Id == id);
            if (existingOrder == null)
            {
                return NotFound();
            }

            existingOrder.ProductName = updatedOrder.ProductName;
            existingOrder.Quantity = updatedOrder.Quantity;

            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public ActionResult DeleteOrder(Guid id)
        {
            var order = Orders.FirstOrDefault(o => o.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            Orders.Remove(order);
            return NoContent();
        }
    }
}
