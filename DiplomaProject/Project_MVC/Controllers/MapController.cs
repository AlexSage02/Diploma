using Microsoft.AspNetCore.Mvc;

namespace Project_MVC.Controllers
{
    public class MapController : Controller
    {
        public IActionResult Index()
        {
            return View("~/Views/Map/Map.cshtml");
        }
    }
}
