using Microsoft.AspNetCore.Mvc;

namespace MVCpractice.Controllers
{
    public class DemoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
