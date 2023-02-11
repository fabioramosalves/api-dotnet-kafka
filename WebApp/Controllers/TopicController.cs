using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class TopicController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
