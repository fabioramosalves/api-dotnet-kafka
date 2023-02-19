using WebApp.Models.Enums;
using WebApp.Services.Kafka.Producer;
using WebApp.Services.Kafka.Consumer;
using WebApp.Models.Consumer;
using WebApp.Models.FlightTracking;

namespace WebApp.Business
{
    public class Message : IMessage
    {
        private readonly IQueueService<FlightTracking> _kafkaProducerService;
        private readonly IKafkaConsumer _kafkaConsumerService;
        public Message(IQueueService<FlightTracking> kafkaProducerService, IKafkaConsumer kafkaConsumerService)
        {
            _kafkaProducerService = kafkaProducerService;
            _kafkaConsumerService = kafkaConsumerService;
        }

        public async Task<FlightTrackingResponse> SendFlightTracking(FlightTrackingRequest flightTrackingRequest)
        {
            var message = flightTrackingRequest.FlightTracking;

            if (message != null)
            {
                _kafkaProducerService.Produce(KafkaTopic.UpdateFlightTracking, KafkaOperation.CREATED, message);
            }

            var result = new FlightTrackingResponse() { HttpCode = 200, HttpMessage = "Success", Success = true };

            return result;
        }

        public async Task<FlightTrackingResponse> GetFlightTracking()
        {
            var result = _kafkaConsumerService.Consume().Result;

            return new FlightTrackingResponse() { HttpCode = 200, HttpMessage = "Success", Success = true, FlightTracking = Mappper(result) };
        }


        private static List<FlightTracking> Mappper(ConsumerResponse response)
        {
            var result = new List<FlightTracking>();

            if (response != null && response.Messages.Any())
            {
                foreach (var item in response.Messages)
                {
                    var flightTracking = new FlightTracking()
                    {
                        AirlineCode = item.AirlineCode,
                        ArrivalAirportCode = item.ArrivalAirportCode,
                        ArrivalLocalTime = item.ArrivalLocalTime,
                        ArrivalTerminal = item.ArrivalTerminal,
                        DepartureAirportCode = item.DepartureAirportCode,
                        DepartureLocalTime = item.DepartureLocalTime,
                        DepartureTerminal = item.DepartureTerminal,
                        LogicalKeyString = item.LogicalKeyString,
                        Number = item.Number,
                        ScheduledDepartureDate = item.ScheduledDepartureDate,
                        Status = item.Status
                    };

                    result.Add(flightTracking);
                }
            }

            return result;
        }
    }
}
