namespace WebApp.Models.FlightTracking
{
    public class FlightTracking
    {
        public string AirlineCode { get; set; }
        public int Number { get; set; }
        public DateTime ScheduledDepartureDate { get; set; }
        public string DepartureAirportCode { get; set; }
        public string ArrivalAirportCode { get; set; }
        public string DepartureTerminal { get; set; }
        public string ArrivalTerminal { get; set; }
        public string Status { get; set; }
        public DateTimeOffset DepartureLocalTime { get; set; }
        public DateTimeOffset ArrivalLocalTime { get; set; }
        public string LogicalKeyString { get; set; }
    }
}
