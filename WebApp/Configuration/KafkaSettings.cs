namespace WebApp.Configuration
{
    public class KafkaSettings
    {
        public bool Active { get; set; }
        public string BootstrapServers { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
