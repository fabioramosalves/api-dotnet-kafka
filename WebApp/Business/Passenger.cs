using WebApp.Models.Passenger;
using WebApp.Models.Enums;
using WebApp.Services.Kafka.Producer;
using WebApp.Services.Kafka.Consumer;
using WebApp.Models.Consumer;

namespace WebApp.Business
{
    public class Message : IMessage
    {
        private readonly IQueueService<Passenger> _kafkaProducerService;
        private readonly IKafkaConsumer _kafkaConsumerService;
        public Message(IQueueService<Passenger> kafkaProducerService, IKafkaConsumer kafkaConsumerService)
        {
            _kafkaProducerService = kafkaProducerService;
            _kafkaConsumerService = kafkaConsumerService;
        }

        public async Task<PassengerResponse> SendPassenger(PassengerRequest passengerRequest)
        {
            var message = passengerRequest.Passenger;

            if (message != null)
            {
                _kafkaProducerService.Produce(KafkaTopic.PassangerTopic, KafkaOperation.CREATED, message);
            }
           

            var result = new PassengerResponse() { HttpCode = 200, HttpMessage = "Success", Success = true };

            return result;
        }

        public async Task<PassengerResponse> GetPassenger()
        {
            var result = _kafkaConsumerService.Consume().Result;

            return new PassengerResponse() { HttpCode = 200, HttpMessage = "Success", Success = true , Passengers = Mappper(result)};
        }


        private static List<Passenger> Mappper(ConsumerResponse response)
        {
            var result = new List<Passenger>();

            if(response != null && response.Messages.Any())
            {
                foreach (var item in response.Messages)
                {
                    var passenger = new Passenger()
                    {
                        Id = item.Id,
                        Name = item.Name,
                        Email = item.Email,
                        PhoneNumber = item.PhoneNumber
                    };

                    result.Add(passenger);
                }
            }

            return result;
        }
    }
}
