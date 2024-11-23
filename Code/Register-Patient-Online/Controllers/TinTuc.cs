using Microsoft.AspNetCore.Mvc;

namespace Register_Patient_Online.Controllers
{
    public class TinTucController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}