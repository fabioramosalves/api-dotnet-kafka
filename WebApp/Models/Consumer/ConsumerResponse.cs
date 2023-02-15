namespace WebApp.Models.Consumer
{
    using Messages;
    public class ConsumerResponse
    {
        public IEnumerable<KafkaMessage> Messages { get; set; }
        public ConsumerResponse()
        {
            Messages = new List<KafkaMessage>();
        }
    }
}
