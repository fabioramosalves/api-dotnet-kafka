namespace WebApp.Configuration
{
    public class KafkaSettings
    {
        public bool Active { get; set; }
        public string BootstrapServers { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int ConnectionAttempts { get; set; }
        public string? GroupId { get; set; }
        public bool EnableAutoCommit { get; set; }
        public string UserNameClient { get; set; }
        public string PasswordClient { get; set; }
    }
}
