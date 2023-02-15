namespace WebApp.Services.Kafka.Consumer
{
    using WebApp.Models.Consumer;
    public interface IKafkaConsumer
    {
        public Task<ConsumerResponse> Consume();
    }
}