using Confluent.Kafka;
using Microsoft.Extensions.Options;
using System.Text.Json;
using WebApp.Configuration;
using WebApp.Models.Consumer;
using WebApp.Models.Enums;
using WebApp.Models.Messages;

namespace WebApp.Services.Kafka.Consumer
{
    public class KafkaConsumer : IKafkaConsumer
    {
        private readonly KafkaSettings _kafkaSettings;
        private readonly ILogger<KafkaConsumer> _logger;
        public KafkaConsumer(IOptions<KafkaSettings> kafkaSettings, ILogger<KafkaConsumer> logger)
        {
            _kafkaSettings = kafkaSettings.Value;
            _logger = logger;
        }
        public async Task<ConsumerResponse> Consume()
        {
            _logger.LogInformation("{Consumer} is running.", nameof(KafkaConsumer));
            var cancelled = false;
            ConsumerResponse result = new();
            var attempsConnection = _kafkaSettings.ConnectionAttempts;
            var cancellationToken = new CancellationTokenSource();
            var topic = KafkaTopic.FlightTrackingTopicUpdateFlight;

            var config = new ConsumerConfig
            {
                BootstrapServers = _kafkaSettings.BootstrapServers,
                GroupId = _kafkaSettings.GroupId,
                SecurityProtocol = SecurityProtocol.SaslSsl,
                SaslMechanism = SaslMechanism.Plain,
                PartitionAssignmentStrategy = PartitionAssignmentStrategy.CooperativeSticky,
                StatisticsIntervalMs = 5000,
                SessionTimeoutMs = 6000,
                AutoOffsetReset = AutoOffsetReset.Earliest,
                EnablePartitionEof = true,
                EnableAutoCommit = _kafkaSettings.EnableAutoCommit,
                SaslUsername = _kafkaSettings.Username,
                SaslPassword = _kafkaSettings.Password,
            };


            using var consumer = new ConsumerBuilder<Ignore, string>(config)
               .SetErrorHandler((_, e) =>
               {
                   _logger.LogError("{Consumer} Error Message: {Message}", nameof(KafkaConsumer), e.Reason);
                   if (e.Code == ErrorCode.Local_Transport)
                   {
                       attempsConnection--;
                   }
                   if (attempsConnection.Equals(0))
                   {
                       cancellationToken.Cancel();
                   }
               })
               .Build();

           
            consumer.Subscribe(_kafkaSettings.Topics.Split(","));
            _logger.LogInformation("{Consumer} suscribed to topics: {Topics}.", nameof(KafkaConsumer), topic);


            try
            {
                while (!cancelled)
                {
                    try
                    {
                        var consume = consumer.Consume(cancellationToken.Token);

                        if (consume.IsPartitionEOF)
                        {
                            _logger.LogInformation(
                                    "Reached end of topic {Topic}", consume.Topic
                                    );
                            cancelled = true;
                        }

                        if (consume.Message != null)
                        {
                            var message = JsonSerializer.Deserialize<FlightTrackingMessage>(consume.Message.Value);
                            if (message != null)
                            {
                                result.Messages = result.Messages.Append(message);
                            }
                        }
                    }
                    catch (ConsumeException e)
                    {
                        _logger.LogError(e, "{Consumer} error Message: {Message}", nameof(KafkaConsumer), e.Error.Reason);
                    }

                }
                consumer.Close();
            }
            catch (OperationCanceledException e)
            {
                _logger.LogError(e, "{Consumer} Error Message: {Message}", nameof(KafkaConsumer), e.Message);
                consumer.Close();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{Consumer} Error Message: {Message}", nameof(KafkaConsumer), e.Message);
                consumer.Close();
            }

            _logger.LogInformation("Closing {Consumer}: {TotalMessages} messages were obtained from all topics", nameof(KafkaConsumer), result.Messages.Count());

            return result;
        }
    }
}
