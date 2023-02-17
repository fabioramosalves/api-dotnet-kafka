using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using WebApp.Business;
using WebApp.Models.FlightTracking;

namespace WebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class TopicController : Controller
    {
        private readonly IMessage _bll;
        public TopicController(IMessage @message) => _bll = @message;

        [HttpPost("FlightTracking")]
        public async Task<IActionResult> PostFlightTracking(FlightTrackingRequest request)
        {
            try
            {
                var result = await _bll.SendFlightTracking(request);

                if (result == null)
                {
                    return NotFound();
                }

                return Ok(result);
            }
            catch (Exception e)
            {
                Log.Error(e, "Service Van: {Action}. Error: {MessageError}", nameof(PostFlightTracking), e.Message);
                throw;
            }
        }

        [HttpGet("FlightTracking")]
        public async Task<IActionResult> GetPassenger()
        {
            try
            {
                var result = await _bll.GetFlightTracking();

                if (result == null)
                {
                    return NotFound();
                }

                return Ok(result);
            }
            catch (Exception e)
            {
                Log.Error(e, "Service Van: {Action}. Error: {MessageError}", nameof(GetPassenger), e.Message);
                throw;
            }
        }
    }
}
