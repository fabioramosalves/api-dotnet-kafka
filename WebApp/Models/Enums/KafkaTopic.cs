namespace WebApp.Models.Enums
{
    using System.ComponentModel;
    public enum KafkaTopic
    {
        [Description("UpdateFlightTracking")]
        UpdateFlightTracking,
        [Description("CreateFlightTracking")]
        CreateFlightTracking,
        [Description("FlightTracking")]
        FlightTracking
    }
}
