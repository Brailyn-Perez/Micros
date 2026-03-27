using Inventory.Consumer.Consumers;
using MassTransit;

namespace Inventory.Consumer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = Host.CreateApplicationBuilder(args);

            // Configuración de MassTransit con RabbitMQ
            builder.Services.AddMassTransit(x =>
            {
                x.AddConsumer<OrdersCreatedConsumer>();

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host("rabbitmq://localhost", h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });

                    cfg.ReceiveEndpoint("order-created-queue", e =>
                    {
                        e.ConfigureConsumer<OrdersCreatedConsumer>(context);
                        e.Bind("order-created-exchange", x =>
                        {
                            x.ExchangeType = "fanout";
                        });
                    });
                });
            });

            var host = builder.Build();
            host.Run();
        }
    }
}