using WebApp.Models.Passenger;

namespace WebApp.Business
{
    public interface IMessage
    {
        Task<PassengerResponse> SendPassenger(PassengerRequest passengerRequest);

        Task<PassengerResponse> GetPassenger();
    }
}