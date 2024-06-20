using Project_BLL;
using Microsoft.AspNetCore.Mvc;
using Project_MVC.Models;
using Microsoft.AspNetCore.Identity;
using Ninject;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json; // Переконайтесь, що у вас є використання EF Core

namespace Project_MVC.Controllers
{
	public class EventController : Controller
	{
		readonly IKernel ninjectKernel = new StandardKernel(new IoC_BLL());

		public IActionResult Events()
		{
			var logic = ninjectKernel.Get<ILogic>();
			var Events = logic.ShowEvent();
			return View(Events);
		}

		public IActionResult Create()
		{
			return View();
		}

		// POST: Create Event
		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize]
		public IActionResult Create(string Name, string Type, string Address, int Quantity, bool Available, DateTime Time, string Latitude, string Longitude)
		{
			var userName = User.Identity.Name;
			var logic = ninjectKernel.Get<ILogic>();

			// Attempt to add the event and handle the result
			var result = logic.AddEvent(Name, Type, Address, Quantity, Available, userName, Time, Latitude, Longitude);

			if (result == false)
			{
				ModelState.AddModelError(string.Empty, "Подія с такими координатами вже iснує");
				return View();
			}

			if (ModelState.IsValid)
			{
				return RedirectToAction("Events");
			}

			return View(Name);
		}


		public IActionResult Delete(string? name)
		{
			var logic = ninjectKernel.Get<ILogic>();
			var events = logic.ShowEvent();
			foreach (var b in events)
			{
				if (b.Name == name)
				{
					EventModel eventModel = new()
					{
						Name = b.Name,
					};
					return View(eventModel);
				}
			}
			return NotFound();
		}

		// POST: Delete Event
		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize]
		public IActionResult DeletePOST(string? name)
		{
			var logic = ninjectKernel.Get<ILogic>();
			logic.RemoveEvent(name);

			return RedirectToAction("Events");
		}

		[HttpGet]
		public IActionResult GetEventLocations()
		{
			var logic = ninjectKernel.Get<ILogic>();
			var events = logic.ShowEvent();
			var locations = events.Select(e => new { e.Latitude, e.Longitude, e.Name, e.Type, e.Address, e.Time }).ToList();

			return Json(locations);
		}
	}
}
