using WebApp.Models.Passenger;
using WebApp.Services.Kafka;
using WebApp.Models.Enums;

namespace WebApp.Business
{
    public class Message : IMessage
    {
        private readonly IQueueService<Passenger> _kafkaService;   
        public Message(IQueueService<Passenger> kafkaService)
        {
            _kafkaService = kafkaService;
        }

        public async Task<PassengerResponse> SendPassenger(PassengerRequest passengerRequest)
        {
            var message = passengerRequest.Passenger;

            if (message != null)
            {
                _kafkaService.Produce(KafkaTopic.PassangerTopic, KafkaOperation.CREATED, message);
            }
           

            var result = new PassengerResponse() { HttpCode = 200, HttpMessage = "Success", Success = true };

            return result;
        }
    }
}
