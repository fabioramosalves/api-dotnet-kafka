using WebApp.Models.Enums;

namespace WebApp.Services.Kafka
{
    public interface IQueueService<T> where T : class
    {
        public void Produce(KafkaTopic topic, KafkaOperation kafkaOperation,T message);
    }
}