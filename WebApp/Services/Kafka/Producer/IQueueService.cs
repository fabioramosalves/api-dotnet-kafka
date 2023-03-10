using WebApp.Models.Enums;

namespace WebApp.Services.Kafka.Producer
{
    public interface IQueueService<T> where T : class
    {
        public void Produce(string topic, KafkaOperation kafkaOperation, T message);
    }
}