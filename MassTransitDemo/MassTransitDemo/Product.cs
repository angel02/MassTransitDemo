using MassTransit;

namespace MassTransitDemo.Messages
{
    [EntityName("product")]
    public class Product
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
