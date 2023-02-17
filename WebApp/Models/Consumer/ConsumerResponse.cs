namespace WebApp.Models.Consumer
{
    using Messages;
    public class ConsumerResponse
    {
        public IEnumerable<FlightTrackingMessage> Messages { get; set; }
        public ConsumerResponse()
        {
            Messages = new List<FlightTrackingMessage>();
        }
    }
}
