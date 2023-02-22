using Microsoft.Extensions.Options;
using WebApp.Models.Enums;
using Confluent.Kafka;
using WebApp.Configuration;
using Newtonsoft.Json;
using Serilog;
using WebApp.Services.Kafka.Producer;
using WebApp.Models.FlightTracking;

namespace WebApp.Services.Kafka
{
    public class KafkaService : IQueueService<FlightTracking>
    {
        private readonly KafkaSettings _kafkaSettings;
        public KafkaService(IOptions<KafkaSettings> kafkaSettings)
        {
            _kafkaSettings = kafkaSettings.Value;
        }

        public async void Produce(string topic, KafkaOperation kafkaOperation, FlightTracking message)
        {
            try
            {
                var config = new ProducerConfig
                {
                    BootstrapServers = _kafkaSettings.BootstrapServers,
                    Acks = Acks.All,
                    EnableDeliveryReports = true,
                    RetryBackoffMs = 1000,
                    MessageSendMaxRetries = 3,
                    SaslUsername = _kafkaSettings.Username,
                    SaslPassword = _kafkaSettings.Password,
                    SaslMechanism = SaslMechanism.Plain,
                    SecurityProtocol = SecurityProtocol.SaslSsl
                };

                using var producer = new ProducerBuilder<long, string>(config).Build();

                long key = DateTime.UtcNow.Ticks;
                var val = JsonConvert.SerializeObject(message);

                string topicValue = topic.ToString();

                var deliveryReport = await producer.ProduceAsync(topicValue, new Message<long, string> { Key = key, Value = val });

                if (deliveryReport.Status != PersistenceStatus.Persisted)
                {
                    Console.WriteLine($"ERROR: Message not ack'd by all brokers. Delivery status: {deliveryReport.Status}");
                }

            }
            catch (ProduceException<long, string> e)
            {
                Log.Error($"Permanent error: {e.Message} for message (value: '{e.DeliveryResult.Value}')");
            }
        }
    }
}
