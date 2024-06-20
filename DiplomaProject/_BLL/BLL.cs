using DAL;
using Ninject;
using Project_DAL;
using Microsoft.AspNetCore.Identity;

namespace Project_BLL
{
	public class BLL : ILogic
	{
		readonly IKernel ninjectKernel = new StandardKernel(new IoC_DAL());

		public bool AddEvent(string Name, string Type, string Address, int Quantity, bool Available, string UserName, DateTime Time, string Latitude, string Longitude)
		{
			var DB = ninjectKernel.Get<IUnitOfWork>();
			var existingEvent = DB.Events.GetAll().FirstOrDefault(e => e.Latitude == Latitude && e.Longitude == Longitude);

			if (existingEvent != null)
			{
				return false;
			}

			Event NewEvent = new Event
			{
				Name = Name,
				Type = Type,
				Address = Address,
				Quantity = Quantity,
				Available = Quantity >= 1,
				IdentityUser = UserName,
				Time = Time,
				Latitude = Latitude,
				Longitude = Longitude
			};

			try
			{
				DB.Events.Create(NewEvent);
				DB.Save();
				return true;
			}
			catch
			{
				return false;
			}
		}



		public void RemoveEvent(string? Name)
		{
			var DB = ninjectKernel.Get<IUnitOfWork>();
			var events = DB.Events.GetAll();

			foreach (Event b in events)
			{
				if (b.Name == Name)
				{
					DB.Events.Delete(b);

				}
			}
			try
			{
				DB.Save();
			}
			catch
			{

			}
		}

		public IEnumerable<Event> ShowEvent()
		{
			var DB = ninjectKernel.Get<IUnitOfWork>();

			var events = DB.Events.GetAll();

			return events;
		}

		public void AddMapEvent(string? Name, string? UserName)
		{
			var DB = ninjectKernel.Get<IUnitOfWork>();

			bool eventsExist = DB.Events.GetAll().Any(b => b.Name == Name && b.Available);
			int mapEventCount = DB.MapEvents.GetAll().Count(f => f.IdentityUser == UserName);

			if (eventsExist && mapEventCount < 10)
			{
				Event @event = DB.Events.GetAll().FirstOrDefault(b => b.Name == Name && b.Available);
				if (@event != null)
				{
					@event.Quantity = 1;
					if (@event.Quantity == 1)
					{
						@event.Available = true;
					}
				}

				MapEvent NewMapEvent = new() { Name = Name, IdentityUser = UserName };

				try
				{
					DB.MapEvents.Create(NewMapEvent);
					DB.Save();
				}
				catch 
				{ 

				}
			}
			else
			{

			}
		}

		public IEnumerable<MapEvent> ShowMapEvent()
		{
			var DB = ninjectKernel.Get<IUnitOfWork>();

			var mapEvents = DB.MapEvents.GetAll();

			return mapEvents;
		}

		public void RemoveMapEvent(string? Name)
		{
			var DB = ninjectKernel.Get<IUnitOfWork>();

			var mapEvent = DB.MapEvents.GetWithInclude();

			foreach (MapEvent a in mapEvent)
			{
				if (a.Name == Name)
				{
                    Event @event = DB.Events.GetAll().FirstOrDefault(b => b.Name == Name);
                    if (@event != null)
                    {
                        @event.Quantity=1;
                        if (@event.Quantity > 0)
                        {
                            @event.Available = true;
                        }
                    }
                    DB.MapEvents.Delete(a);
				}
			}
			try
			{
				DB.Save();
			}
			catch
			{

			}
		}

	}
}