using MassTransit;
using MassTransitDemo.Messages;

namespace MassTransitDemo.Consumer.Consumers
{
    public class ProductConsumer : IConsumer<Product>
    {
        public async Task Consume(ConsumeContext<Product> context)
        {
            var data = context.Message;

            Console.WriteLine(data.Name);
        }
    }
}
