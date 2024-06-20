using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ninject;
using Project_BLL;
using Project_MVC.Models;

namespace Project_MVC.Controllers
{
    public class MapEventController : Controller
    {
        readonly IKernel ninjectKernel = new StandardKernel(new IoC_BLL());

        public IActionResult Index()
        {
            var logic = ninjectKernel.Get<ILogic>();
            var MapEvents = logic.ShowMapEvent();
            return View(MapEvents);
        }
        public IActionResult Create()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
		[Authorize]
		public IActionResult Create(string Name)
        {
			var userName = User.Identity.Name;

			if (ModelState.IsValid)
            {
                var logic = ninjectKernel.Get<ILogic>();
                logic.AddMapEvent(Name, userName);

                return RedirectToAction("Index");
            }
            return View(Name);
        }

        public IActionResult Delete(string? name)
        {
            var logic = ninjectKernel.Get<ILogic>();
            var MapEvents = logic.ShowMapEvent();
            foreach (var f in MapEvents)
            {
                if (f.Name == name)
                {
                    MapEventModel mapEventModel = new()
                    {
                        Name = f.Name,
                    };
                    return View(mapEventModel);
                }
            }
            return NotFound();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
		[Authorize]
		public IActionResult DeletePOST(string? name)
        {
            var logic = ninjectKernel.Get<ILogic>();
            logic.RemoveMapEvent(name);

            return RedirectToAction("Index");
        }
    }
}
