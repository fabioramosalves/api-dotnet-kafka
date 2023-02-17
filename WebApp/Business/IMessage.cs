using WebApp.Models.FlightTracking;

namespace WebApp.Business
{
    public interface IMessage
    {
        Task<FlightTrackingResponse> SendFlightTracking(FlightTrackingRequest passengerRequest);
        Task<FlightTrackingResponse> GetFlightTracking();
    }
}