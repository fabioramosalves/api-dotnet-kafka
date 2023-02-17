namespace WebApp.Models.FlightTracking
{
    public class FlightTrackingResponse
    {
        public bool Success { get; set; }
        public string HttpMessage { get; set; }
        public int HttpCode { get; set; }
        public List<FlightTracking> FlightTracking { get; set; }
        public int Total => FlightTracking != null && FlightTracking.Any() ? FlightTracking.Count : 0;
    }
}
