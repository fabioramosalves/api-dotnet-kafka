namespace WebApp.Models.Passenger
{
    public class PassengerResponse
    {
        public bool Success { get; set; }
        public string HttpMessage { get; set; }
        public int HttpCode { get; set; }
        public List<Passenger> Passengers { get; set; }
    }
}
