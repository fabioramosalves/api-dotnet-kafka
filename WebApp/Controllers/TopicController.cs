using Microsoft.AspNetCore.Mvc;
using Serilog;
using WebApp.Business;
using WebApp.Models.Passenger;

namespace WebApp.Controllers
{

    public class TopicController : Controller
    {
        private readonly IMessage _bll;
        public TopicController(IMessage @message) => _bll = @message;
        public async Task<IActionResult> PostPassenger(PassengerRequest request)
        {
            try
            {
                var result = await _bll.SendPassenger(request);

                if (result == null)
                {
                    return NotFound();
                }

                return Ok(result);
            }
            catch (Exception e)
            {
                Log.Error(e, "Service Van: {Action}. Error: {MessageError}", nameof(PostPassenger), e.Message);
                throw;
            }
        }
    }
}
