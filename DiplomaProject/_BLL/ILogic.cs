using DAL;

namespace Project_BLL
{
    public interface ILogic
    {
        public bool AddEvent(string? Name, string? Type, string? Address, int Quantity, bool Available, string? UserName, DateTime Time, string? Latitude, string? Longitude);

		public IEnumerable<Event> ShowEvent();

        void RemoveEvent(string? Name);

        public void AddMapEvent(string? Name, string? UserName);

        public IEnumerable<MapEvent> ShowMapEvent();

        void RemoveMapEvent(string? Name);
	}
}